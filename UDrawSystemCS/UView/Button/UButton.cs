using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using UDrawSystemCS.UDraw;

namespace UDrawSystemCS.UView.Button
{
    /// <summary>
    /// ボタンが押された時に呼ばれるデリゲート
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public delegate int ButtonCallback(UButton button);

    public class UButton : UDrawable
    {
        //
        // Const
        // 
        private static Color DefaultTextColor = Color.Black;
        private static Color DefaultPressedTextColor = Color.White;
        private static Color DefaultBgColor = Color.Blue;
        private static Color DefaultHoverColor = Color.LightBlue;
        private static Color DefaultDisabledColor = Color.Gray;
        private static Color DefaultPressedColor = Color.DarkBlue;

        private static Font DefaultFont;
        private static Brush DefaultTextBrush;

        //
        // Static variable
        //

        private static StringFormat sf;

        static UButton()
        {
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            DefaultFont = new Font(UDrawUtility.FontName, 10);
            DefaultTextBrush = UBrushManager.getBrush(DefaultTextColor);
        }

        //
        // Property
        //
        protected Font font;
        protected Brush textBrush;

        protected int id;
        public int Id
        {
            get { return id; }
        }

        protected string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        protected bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        protected bool pressed;
        public bool Pressed
        {
            get { return pressed; }
            set { pressed = value; }
        }

        protected Color textColor;
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        protected Color colorTextPressed;
        public Color ColorTextPressed
        {
            get { return colorTextPressed; }
            set { colorTextPressed = value; }
        }

        protected Color bgColor;
        public Color BgColor
        {
            get { return bgColor; }
            set { bgColor = value; }
        }

        protected Color colorHover;
        public Color ColorHover
        {
            get { return colorHover; }
            set { colorHover = value; }
        }

        protected Color colorDisabled;
        public Color ColorDisabled
        {
            get { return colorDisabled; }
            set { colorDisabled = value; }
        }

        protected Color colorPressed;
        public Color ColorPressed
        {
            get { return colorPressed; }
            set { colorPressed = value; }
        }

        protected ButtonCallback callback;
        public ButtonCallback Callback
        {
            get { return callback; }
            set { callback = value; }
        }


        // 
        // Method
        // 

        /**
         * Constructor
         */
        public UButton(int priority, int id, string text, float x, float y, int width, int height, ButtonCallback callback)
            : base(priority, text, x, y, width, height)
        {
            this.callback = callback;
            this.id = id;
            this.text = text;

            enabled = true;
            textColor = DefaultTextColor;
            colorTextPressed = DefaultPressedTextColor;
            bgColor = DefaultBgColor;
            colorHover = DefaultHoverColor;
            colorDisabled = DefaultDisabledColor;
            colorPressed = DefaultPressedColor;
            font = DefaultFont;
            textBrush = DefaultTextBrush;
        }
        
        /**
         * 描画処理
         * @param canvas
         * @param paint
         * @param offset 独自の座標系を持つオブジェクトをスクリーン座標系に変換するためのオフセット値
         */
        override public void draw(Graphics g, PointF offset)
        {
            Color _textColor, _bgColor;

            _textColor = textColor;

            if (enabled == false)
            {   
                _bgColor = colorDisabled;
            }
            else if (isHover)
            {
                _bgColor = colorHover;
            }
            else if (pressed)
            {
                _textColor = colorTextPressed;
                _bgColor = colorPressed;
            }
            else
            {
                _bgColor = bgColor;
            }

            // bg
            g.FillRectangle(new SolidBrush(_bgColor), pos.X, pos.Y, size.Width, size.Height);

            // text
            textBrush = UBrushManager.getBrush(_textColor);
            g.DrawString(text, DefaultFont, textBrush, pos.X + size.Width / 2, pos.Y + size.Height / 2, sf);
        }



        /**
         * タッチ処理
         * @param vt
         * @return
         */
        override public bool touchEvent(ViewTouch vt, PointF offset, out bool isHover)
        {
            isHover = false;
            // 領域内をマウスダウンしたらtrueを返す
            if (checkInside(vt.args.Location))
            {
                switch (vt.MEvent)
                {
                    case MouseEvent.Down:
                        pressed = true;
                        break;
                    case MouseEvent.Click:
                        // クリック時の処理
                        if (callback != null)
                        {
                            callback(this);
                        }
                        return true;
                    case MouseEvent.Move:
                        // ホバー時の処理
                        isHover = true;
                        return true;
                }
                return true;
            }
            else
            {
                pressed = false;
            }
            return false;
        }

    }
}
