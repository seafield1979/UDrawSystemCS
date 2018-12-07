using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UDrawSystemCS.UDraw;
using UDrawSystemCS.UUtil;

namespace UDrawSystemCS.UView.LogView
{
    /*
     * 拡大率を管理するクラス
     */
    public class ZoomRate
    {
        enum EZoomRate : byte
        {
            E50P = 0,
            E67P,
            E75P,
            E80P,
            E90P,
            E100P,  // 100%
            E110P,
            E125P,
            E150P,
            E175P,
            E200P,
            E250P,
            E300P,
            E400P
        }
        private EZoomRate zoomRate;

        private float _value;

        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }

        //
        // Consts
        //
        private float[] eToV = new float[] { 0.5f, 0.67f, 0.75f, 0.8f, 0.9f, 1.0f, 1.1f, 1.25f, 1.5f, 1.75f, 2.0f, 2.5f, 3.0f, 4.0f };

        //
        // Constructor
        // 
        public ZoomRate()
        {
            zoomRate = EZoomRate.E100P;
            SetZoomValue();
        }

        private void SetZoomValue()
        {
            _value = eToV[(byte)zoomRate];
        }

        public float ZoomIn()
        {
            if (zoomRate < EZoomRate.E400P)
            {
                zoomRate++;
            }
            SetZoomValue();
            return _value;
        }

        public float ZoomOut()
        {
            if (zoomRate > EZoomRate.E50P)
            {
                zoomRate--;
            }
            SetZoomValue();
            return _value;
        }

        public override string ToString()
        {
            return string.Format("{0}%", (int)(_value * 100));
        }
    }

    /// <summary>
    /// 画面全体のズーム率を管理するクラス
    /// </summary>
    class ZoomView : UDrawable
    {
        //
        // Const
        // 
        private const int smallButtonW = 20;
        private const int centerButtonW = 50;
        private const int centerButtonH = 20;
        private const int marginX = 5;
        private const int fontSize = 10;
    
        //
        // Property
        //
        public ZoomRate zoom;
        private Font font1;
        private Brush brush1, brush2, brush3, hoverBrush1, hoverBrush2, hoverBrush3;
        private Rectangle rect1, rect2, rect3;
        private int hoverNo;
        
        //
        // Constructor
        //
        public ZoomView(int priority, string name, float x, float y) 
            : base(priority, name, x, y, 0, 0)
        {
            font1 = UFontManager.getFont(UDrawUtility.FontName, fontSize);
            brush1 = Brushes.DarkBlue;
            brush2 = Brushes.DarkRed;
            brush3 = Brushes.LightGray;
            hoverBrush1 = new SolidBrush(Color.FromArgb(UColor.setBrightness(Color.DarkBlue.ToArgb(), 128)));
            hoverBrush2 = new SolidBrush(Color.FromArgb(UColor.setBrightness(Color.DarkRed.ToArgb(), 128)));
            hoverBrush3 = new SolidBrush(Color.FromArgb(UColor.setBrightness(Color.Gray.ToArgb(), 128)));
            zoom = new ZoomRate();
            int _x = (int)pos.X;
            rect1 = new Rectangle(_x, (int)pos.Y, smallButtonW, smallButtonW);
            _x += smallButtonW + marginX;
            rect2 = new Rectangle(_x, (int)pos.Y, centerButtonW, centerButtonH);
            _x += centerButtonW + marginX;
            rect3 = new Rectangle(_x, (int)pos.Y, smallButtonW, smallButtonW);
        }

        //
        // method
        // 
        public override void draw(Graphics g, PointF offset)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;

            Brush brush;

            // -ボタン
            brush = (isHover && hoverNo == 1) ? hoverBrush1 : brush1;
            g.FillRectangle(brush, x, y, smallButtonW, smallButtonW);
            UDraw.UDraw.drawText(g, "-", StringAlignment.Center, StringAlignment.Center, false, font1, x + smallButtonW / 2, y + smallButtonW / 2, Color.White);

            x += smallButtonW + marginX;

            // zoom rateボタン
            brush = (isHover && hoverNo == 2) ? hoverBrush3 : brush3;
            g.FillRectangle(brush, x, y, centerButtonW, centerButtonH);
            UDraw.UDraw.drawText(g, zoom.ToString(), StringAlignment.Center, StringAlignment.Center, false, font1, x + centerButtonW / 2, y + centerButtonH / 2, Color.Black);

            x += centerButtonW + marginX;

            // +ボタン
            brush = (isHover && hoverNo == 3) ? hoverBrush2 : brush2;
            g.FillRectangle(brush, x, y, smallButtonW, smallButtonW);
            UDraw.UDraw.drawText(g, "+", StringAlignment.Center, StringAlignment.Center, false, font1, x + smallButtonW / 2, y + smallButtonW / 2, Color.White);
        }

        public override bool touchEvent(ViewTouch vt, PointF offset, out bool isHover)
        {
            isHover = false;
            // 領域内をマウスダウンしたらtrueを返す
            switch (vt.MEvent)
            {
                case MouseEvent.Click:
                    // クリック時の処理
                    if (UDrawUtility.checkInside(vt.args.Location, rect1))
                    {
                        Console.WriteLine("1");
                        return true;
                    }
                    else if (UDrawUtility.checkInside(vt.args.Location, rect2))
                    {
                        Console.WriteLine("2");
                        return true;
                    }
                    else if (UDrawUtility.checkInside(vt.args.Location, rect3))
                    {
                        Console.WriteLine("3");
                        return true;
                    }
                    break;
                case MouseEvent.Move:
                    // ホバー時の処理
                    if (UDrawUtility.checkInside(vt.args.Location, rect1))
                    {
                        isHover = true;
                        hoverNo = 1;
                        return true;
                    }
                    else if (UDrawUtility.checkInside(vt.args.Location, rect2))
                    {
                        isHover = true;
                        hoverNo = 2;
                        return true;
                    }
                    else if (UDrawUtility.checkInside(vt.args.Location, rect3))
                    {
                        isHover = true;
                        hoverNo = 3;
                        return true;
                    }
                    else
                    {
                        isHover = false;
                        hoverNo = 0;
                    }
                    break;
                }
            return false;
        }
    }
}
