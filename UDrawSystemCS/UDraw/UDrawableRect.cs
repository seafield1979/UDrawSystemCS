using System.Drawing;
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
            // todo
            //paint.setColor(color);
            //paint.setStyle(Paint.Style.FILL);
            //canvas.drawRect(pos.x, pos.y, pos.x + size.width, pos.y + size.height, paint);
        }

        /**
         * タッチ処理
         * @param vt
         * @return
         */
        public bool touchEvent(ViewTouch vt, PointF offset)
        {
            return false;
        }
    }
}
