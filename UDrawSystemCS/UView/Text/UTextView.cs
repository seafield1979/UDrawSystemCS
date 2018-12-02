using System;
using System.Drawing;
using UDrawSystemCS.UDraw;


namespace UDrawSystemCS.UView.Text
{
    /**
     * テキストを表示する
     */
    public class UTextView : UDrawable
    {
        /**
         * Constracts
         */
        // BGを描画する際の上下左右のマージン
        protected const int MARGIN_H = 10;
        protected const int MARGIN_V = 5;

        protected const int DEFAULT_TEXT_SIZE = 17;
        protected static Color DEFAULT_COLOR = Color.Black;
        protected static Color DEFAULT_BG_COLOR = Color.White;

        /**
         * Member variables
         */
        protected String text;
        protected StringAlignment alignX;
        protected StringAlignment alignY;
        protected int fontSize;
        protected bool multiLine;      // 複数行表示する
        protected Font font;
        
        /**
         * Get/Set
         */
        public String getText()
        {
            return text;
        }
        public void setText(String text)
        {
            this.text = text;
        }

        /**
         * Constructor
         */
        public UTextView(String text, int fontSize, int priority,
                         StringAlignment alignX, StringAlignment alignY,
                         bool multiLine, float x, float y,
                         Color color) : base(priority, text, x, y, 0, 0)
        {   

            this.text = text;
            this.alignX = alignX;
            this.alignY = alignY;
            this.multiLine = multiLine;
            this.fontSize = fontSize;
            this.color = color;

            font = UFontManager.getFont(UDrawUtility.FontName, fontSize);
        }

        public static UTextView createInstance(String text, int fontSize, int priority,
                                               bool multiLine,
                                               float x, float y,
                                               Color color)
        {
            UTextView instance = new UTextView(text, fontSize, priority, StringAlignment.Near, StringAlignment.Near, 
                    multiLine, x, y, color);

            return instance;
        }

        // シンプルなTextViewを作成
        public static UTextView createInstance(String text, int priority,
                                               float x, float y)
        {
            UTextView instance = new UTextView(text, DEFAULT_TEXT_SIZE, priority, StringAlignment.Near, StringAlignment.Near,
                    false, 
                    x, y, DEFAULT_COLOR);
            return instance;
        }

        /**
         * Methods
         */
        
        /**
         * 描画処理
         * @param canvas
         * @param paint
         * @param offset 独自の座標系を持つオブジェクトをスクリーン座標系に変換するためのオフセット値
         */
        override public void draw(Graphics g, PointF offset)
        {
            PointF _pos = new PointF(pos.X, pos.Y);
            if (offset != null)
            {
                _pos.X += offset.X;
                _pos.Y += offset.Y;
            }
            PointF _linePos = new PointF(_pos.X, _pos.Y);

            Color _color;
            if (isHover)
            {
                _color = Color.Azure;
            }
            else
            {
                _color = color;
            }
            UDraw.UDraw.drawText(g, text, alignX, alignY, multiLine, font, _pos.X, _pos.Y, _color);

            // サイズが設定されていないなら設定
            if (size == Size.Empty)
            {
                size = g.MeasureString(text, font, 1000).ToSize();
            }
        }
        
        /**
         * 矩形を取得
         * @return
         */
        override public Rectangle getRect()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)pos.X + size.Width, (int)pos.Y + size.Height);
        }

        /**
         * タッチ処理
         * @param vt
         * @return
         */
        public bool touchEvent(ViewTouch vt, out bool isHover)
        {
            return this.touchEvent(vt, PointF.Empty, out isHover);
        }

        override public bool touchEvent(ViewTouch vt, PointF offset,out bool isHover)
        {
            if(vt.MEvent == MouseEvent.Move)
            {
                if (checkInside(vt.args.Location))
                {
                    isHover = true;
                    return true;
                }
            }
            isHover = false;
            return false;
        }
    }

}
