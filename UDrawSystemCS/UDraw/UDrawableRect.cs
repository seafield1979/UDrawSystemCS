using System.Drawing;
using System.Windows.Forms;
using UDrawSystemCS.UView;

namespace UDrawSystemCS.UDraw
{
    public class UDrawableRect : UDrawable
    {
        /**
         * Constructor
         */
        public UDrawableRect(int priority, string name, float x, float y, int width, int height) : base(priority, name, x, y, width, height)
        {   
        }

        public static UDrawableRect createInstance(int priority, string name, float x, float y, int width, int
                height, Color color)
        {
            UDrawableRect instance = new UDrawableRect(priority, name, x, y, width, height);
            instance.color = color;
            return instance;
        }

        /**
         * 描画処理
         * @param canvas
         * @param paint
         * @param offset 独自の座標系を持つオブジェクトをスクリーン座標系に変換するためのオフセット値
         */
        override public void draw(Graphics g, PointF offset)
        {
            Color _color;
            if (isAnimating)
            {
                _color = Color.FromArgb((int)(animeRatio * 128.0f), color);
            }
            else if (isHover)
            {
                _color = Color.FromArgb(64, color);
            }
            else
            {
                _color = Color;
            }

            g.FillRectangle(new SolidBrush(_color), pos.X, pos.Y, size.Width, size.Height);
#if DEBUG && true
            g.DrawString(this.name, debugFont, debugBrush, this.pos.X, this.pos.Y);
#endif
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
                switch(vt.MEvent)
                {
                    case MouseEvent.Click:
                        // クリック時の処理
                        System.Console.WriteLine("touch UDrawRectangle:" + name);
                        this.startAnimation(100);
                        return true;
                    case MouseEvent.Move:
                        // ホバー時の処理
                        isHover = true;
                        return true;
                }
                

                return true;
            }
            return false;
        }
    }
}
