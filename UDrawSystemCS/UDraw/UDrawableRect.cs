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
        public UDrawableRect(int priority, float x, float y, int width, int height) : base(priority, x, y, width, height)
        {   
        }

        public static UDrawableRect createInstance(int priority, float x, float y, int width, int
                height, Color color)
        {
            UDrawableRect instance = new UDrawableRect(priority, x, y, width, height);
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
                System.Console.WriteLine("touch UDrawRectangle");
                return true;
            }
            return false;
        }
    }
}
