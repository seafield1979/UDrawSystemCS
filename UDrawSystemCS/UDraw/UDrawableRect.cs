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
            g.FillRectangle(new SolidBrush(color), pos.X, pos.Y, size.Width, size.Height);
#if DEBUG && true
            g.DrawString(this.name, debugFont, debugBrush, this.pos.X, this.pos.Y);
#endif
        }

        /**
         * タッチ処理
         * @param vt
         * @return
         */
        override public bool touchEvent(ViewTouch vt, PointF offset)
        {
            Point mpos = vt.args.Location;
            // 領域内をマウスダウンしたらtrueを返す
            if (pos.X <= mpos.X && mpos.X <= pos.X + Width && 
                pos.Y <= mpos.Y && mpos.Y <= pos.Y + Height )
            {
                System.Console.WriteLine("touch UDrawRectangle:" + name);

                this.startMoving(0, 0, 100);

                return true;
            }
            return false;
        }
    }
}
