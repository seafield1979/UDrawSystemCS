using System;
using System.Drawing;
using UDrawSystemCS.UDraw;
using UDrawSystemCS.UUtil;

namespace UDrawSystemCS.UView
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
        protected UAlignment alignment;
        protected Size mMargin;
        protected int textSize;
        protected Color bgColor;
        protected int canvasW;
        protected bool multiLine;      // 複数行表示する

        protected bool isDrawBG;
        protected bool isOpened;

        /**
         * Get/Set
         */
        public String getText()
        {
            return text;
        }
        public void setText(Graphics g, String text)
        {
            this.text = text;

            // サイズを更新
            Size size = getTextSize(g, canvasW);
            if (isDrawBG)
            {
                setSize(size.Width + mMargin.Width * 2, size.Height + mMargin.Height * 2);
            }
            else
            {
                setSize(size.Width, size.Height);
            }
            updateRect();
        }

        public void setMargin(Graphics g, int width, int height)
        {
            mMargin.Width = width;
            mMargin.Height = height;
            updateSize(g);
        }

        /**
         * Constructor
         */
        public UTextView(Graphics g, String text, int textSize, int priority,
                         UAlignment alignment, int canvasW,
                         bool multiLine, bool isDrawBG, bool marginH,
                         float x, float y,
                         int width,
                         Color color, Color bgColor) : base(priority, x, y, width, textSize)
        {
            
            this.text = text;
            this.alignment = alignment;
            this.multiLine = multiLine;
            this.isDrawBG = isDrawBG;
            this.textSize = textSize;
            this.canvasW = canvasW;
            this.color = color;
            this.bgColor = bgColor;

            // テキストを描画した時のサイズを取得
            if (width == 0)
            {
                size = getTextSize(g, canvasW);
            }
            mMargin = new Size(MARGIN_H, MARGIN_V);

            updateSize(g);
        }

        public static UTextView createInstance(Graphics g, String text, int textSize, int priority,
                                               UAlignment alignment, int canvasW,
                                               bool multiLine, bool isDrawBG,
                                               float x, float y,
                                               int width,
                                               Color color, Color bgColor)
        {
            UTextView instance = new UTextView(g, text, textSize, priority, alignment, canvasW,
                    multiLine, isDrawBG, true,
                    x, y, width, color, bgColor);

            return instance;
        }

        // シンプルなTextViewを作成
        public static UTextView createInstance(Graphics g, String text, int priority,
                                               int canvasW, bool isDrawBG,
                                               float x, float y)
        {
            UTextView instance = new UTextView(g, text, DEFAULT_TEXT_SIZE, priority, UAlignment.None,
                    canvasW, false, isDrawBG, true,
                    x, y, 0, DEFAULT_COLOR, DEFAULT_BG_COLOR);
            return instance;
        }

        /**
         * Methods
         */

        protected void updateSize(Graphics g)
        {
            Size size = getTextSize(g, canvasW);
            if (isDrawBG)
            {
                size = addBGPadding(size);
            }
            setSize(size.Width, size.Height);
        }

        /**
         * テキストを囲むボタン部分のマージンを追加する
         * @param size
         * @return マージンを追加した Size
         */
        protected Size addBGPadding(Size size)
        {
            if (size == null)
            {
                return new Size(0, 0);
            }
            size.Width += mMargin.Width * 2;
            size.Height += mMargin.Height * 2;
            return size;
        }


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

            UAlignment _alignment = alignment;

            if (isDrawBG)
            {
                PointF bgPos = new PointF(_pos.X, _pos.Y);
                switch (alignment)
                {
                    case UAlignment.CenterX:
                        bgPos.X -= size.Width / 2;
                        _pos.Y += mMargin.Height;
                        break;
                    case UAlignment.CenterY:
                        _pos.X += mMargin.Height;
                        bgPos.Y -= size.Height / 2;
                        break;
                    case UAlignment.Center:
                        bgPos.X -= size.Width / 2;
                        bgPos.Y -= size.Height / 2;
                        break;
                    case UAlignment.None:
                        _pos.X += mMargin.Width;
                        _pos.Y += mMargin.Height;
                        break;
                    case UAlignment.Right:
                        bgPos.X -= size.Width;
                        _pos.X -= mMargin.Width;
                        _pos.Y += mMargin.Height;
                        break;
                    case UAlignment.Right_CenterY:
                        bgPos.X -= size.Width;
                        _pos.X -= mMargin.Width;
                        bgPos.Y -= size.Height / 2;
                        break;
                }

                if (!multiLine)
                {
                    if (alignment == UAlignment.CenterX ||
                            alignment == UAlignment.None ||
                            alignment == UAlignment.Right)
                    {
                        _pos.Y += textSize / 2;
                    }
                }

                // Background
                if (isDrawBG && bgColor != Color.Transparent)
                {
                    drawBG(g, bgPos);
                }

                // BGの中央にテキストを表示したいため、aligmentを書き換える
                if (!multiLine)
                {
                    switch (alignment)
                    {
                        case UAlignment.CenterX:
                            _alignment = UAlignment.Center;
                            break;
                        case UAlignment.None:
                            _alignment = UAlignment.CenterY;
                            break;
                        case UAlignment.Right:
                            _alignment = UAlignment.Right_CenterY;
                            break;
                    }
                }
            }
            if (multiLine)
            {
                UDraw.UDraw.drawText(g, text, _alignment, textSize, _pos.X, _pos.Y, color);
                
            }
            else
            {
                UDraw.UDraw.drawTextOneLine(g, text, _alignment, textSize,
                        _pos.X, _pos.Y,
                        color);
            }

            // x,yにラインを表示 for Debug
            if (UDebug.drawTextBaseLine)
            {
                UDraw.UDraw.drawLine(g, _linePos.X - 50, _linePos.Y,
                        _linePos.X + 50, _linePos.Y, 3, Color.Red);
                UDraw.UDraw.drawLine(g, _linePos.X, _linePos.Y - 50,
                        _linePos.X, _linePos.Y + 50, 3, Color.Red);
            }
        }

        /**
         * 背景色を描画する
         * @param canvas
         * @param paint
         */
        protected void drawBG(Graphics g, PointF pos)
        {
            if (multiLine)
            {
                //paint.setColor(bgColor);
                UDraw.UDraw.drawRoundRectFill(g,
                        new RectangleF(pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Height),
                        7, bgColor, 0, Color.Transparent);
            }
            else
            {
                UDraw.UDraw.drawRoundRectFill(g,
                        new RectangleF(pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Height),
                        7, bgColor, 0, Color.Transparent);
            }
        }

        /**
         * テキストのサイズを取得する（マルチライン対応）
         * @param canvasW
         * @return
         */
        public Size getTextSize(Graphics g, int canvasW)
        {

            if (multiLine)
            {
                return UDraw.UDraw.getTextSize(canvasW, text, textSize);
            }
            else
            {
                return UDraw.UDraw.getOneLineTextSize(g, text, textSize);
            }
        }

        /**
         * 矩形を取得
         * @return
         */
        public Rectangle getRect()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)pos.X + size.Width, (int)pos.Y +
                    size.Height);
        }

        /**
         * タッチ処理
         * @param vt
         * @return
         */
        public bool touchEvent(ViewTouch vt)
        {
            return this.touchEvent(vt, PointF.Empty);
        }

        override public bool touchEvent(ViewTouch vt, PointF offset)
        {
            return false;
        }
    }

}
