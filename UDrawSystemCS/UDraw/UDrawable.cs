using System;
using System.Drawing;
using System.Windows.Forms;
using UDrawSystemCS.UView;
using UDrawSystemCS.UUtil;

namespace UDrawSystemCS.UDraw
{
    abstract public class UDrawable
    {
        /**
         * Enums
         */
        // 自動移動のタイプ
        public enum MovingType
        {
            UniformMotion,      // 等速運動
            Acceleration,       // 加速
            Deceleration        // 減速
        }


        /**
         * Constants
         */
        private const String TAG = "UDrawable";
        public static double RAD = 3.1415 / 180.0;

        /**
         * Static variables
         */
        protected static Font debugFont = new Font("MS UI Gothic", 10);
        protected static Brush debugBrush = Brushes.Black;

        /**
         * Member variables
         */
        protected DrawList drawList;    // DrawManagerに描画登録するとnull以外になる
        protected PointF pos = new PointF();
        protected Size size = new Size();
        protected Rectangle rect = new Rectangle();
        protected string name;
        public int drawPriority;     // DrawManagerに渡す描画優先度

        // 自動移動、サイズ変更、色変更
        protected bool isMoving;
        protected bool isMovingPos;
        protected bool isMovingSize;

        protected bool _isShow;

        public bool isShow
        {
            get { return _isShow; }
            set { _isShow = value; }
        }

        protected MovingType movingType;
        protected int movingFrame;
        protected int movingFrameMax;

        protected PointF srcPos = new PointF();
        protected PointF dstPos = new PointF();

        protected Size srcSize = new Size();
        protected Size dstSize = new Size();

        protected Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }


        // アニメーション用
        public const int ANIME_FRAME = 20;
        protected bool _isAnimating;

        public bool isAnimating
        {
            get { return _isAnimating; }
            set { _isAnimating = value; }
        }

        protected int animeFrame;
        protected int animeFrameMax;
        protected float animeRatio;

        public UDrawable(int priority, string name, float x, float y, int width, int height)
        {
            this.name = name;
            this.setPos(x, y);
            this.setSize(width, height);
            updateRect();

            this.drawPriority = priority;
            this.color = Color.FromArgb(255, 0, 0, 0);
            isShow = true;
        }

        /**
         *  Get/Set
         */
        public float getX()
        {
            return pos.X;
        }
        public void setX(float x)
        {
            pos.X = x;
        }

        public float getY()
        {
            return pos.Y;
        }
        public void setY(float y)
        {
            pos.Y = y;
        }

        public PointF getPos()
        {
            return pos;
        }

        public void setPos(float x, float y)
        {
            setPos(x, y, true);
        }
        public void setPos(float x, float y, bool update)
        {
            pos.X = x;
            pos.Y = y;
            if (rect != null && update)
            {
                updateRect();
            }
        }
        public void setPos(PointF pos)
        {
            setPos(pos, true);
        }

        public void setPos(PointF pos, bool update)
        {
            this.pos.X = pos.X;
            this.pos.Y = pos.Y;
            updateRect();
        }
        public Size getSize()
        {
            return size;
        }

        public void updateRect()
        {
            if (rect == null)
            {
                rect = new Rectangle((int)pos.X, (int)pos.Y, (int)pos.X + size.Width, (int)pos.Y + size.Height);
            }
            else
            {
                rect.X = (int)pos.X;
                rect.Width = size.Width;
                rect.Y = (int)pos.Y;
                rect.Height = size.Height;
            }
        }
        public bool getIsShow()
        {
            return isShow;
        }

        /**
         * Rectをスケールする。ボタン等のタッチ範囲を広げるのに使用する
         * @param scaleH
         * @param scaleV
         */
        public void scaleRect(float scaleH, float scaleV)
        {
            float _scaleW = size.Width * (scaleH - 1.0f) / 2;
            float _scaleH = size.Height * (scaleV - 1.0f) / 2;

            rect.X = (int)(pos.X + -_scaleW);
            rect.Y = (int)(pos.Y + -_scaleH);
            rect.Width = (int)(size.Width + _scaleW);
            rect.Height = (int)( size.Height + _scaleH);
        }

        
        public float Right
        {
            get { return pos.X + size.Width; }
        }

        public float Bottom
        {
            get { return pos.Y + size.Height; }
        }

        public int Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }

        public int Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }
        
        public void setSize(int width, int height)
        {
            size.Width = width;
            size.Height = height;
            updateRect();
        }
        public Rectangle getRect() { return rect; }
        public Rectangle getRectWithOffset(PointF offset)
        {
            return new Rectangle(rect.Left + (int)offset.X, rect.Top + (int)offset.Y,
                    rect.Right + (int)offset.X, rect.Bottom + (int)offset.Y);
        }
        public RectangleF getRectF()
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }

        // 枠の分太いRectを返す
        public Rectangle getRectWithOffset(PointF offset, int frameWidth)
        {
            return new Rectangle(rect.Left + (int)offset.X - frameWidth,
                    rect.Top + (int)offset.Y - frameWidth,
                    rect.Right + (int)offset.X + frameWidth,
                    rect.Bottom + (int)offset.Y + frameWidth);
        }

        

        /**
         * 後処理。nullを設定する前に呼ぶ
         */
        public void cleanUp()
        {
            UDrawManager.getInstance().removeDrawable(this);
        }

        /**
         * 描画処理
         * @param canvas
         * @param paint
         * @param offset 独自の座標系を持つオブジェクトをスクリーン座標系に変換するためのオフセット値
         */
        abstract public void draw(Graphics g, PointF offset);

        /**
         * 毎フレームの処理
         * サブクラスでオーバーライドして使用する
         * @return true:処理中 / false:処理完了
         */
        public DoActionRet doAction()
        {
            return DoActionRet.None;
        }

        /**
         * Rectをライン描画する for Debug
         */
        public void drawRectLine(Graphics g, PointF offset, int color)
        {
            Rectangle _rect = new Rectangle(rect.Left + (int)offset.X,
                    rect.Top + (int)offset.Y,
                    rect.Right + (int)offset.X,
                    rect.Bottom + (int)offset.Y);
            UDraw.drawRect(g, _rect, 2, color);
        }

        /**
         * タッチアップ処理
         * @param vt
         * @return
         */
        virtual public bool touchUpEvent(ViewTouch vt) { return false; }

        /**
         * タッチ処理
         * @param vt
         * @return
         */
        virtual public bool touchEvent(ViewTouch vt, PointF offset)
        {
            return false;
        }

        /**
         * DrawManagerの描画リストに追加する
         */
        public void addToDrawManager()
        {
            UDrawManager.getInstance().addDrawable(this);
        }

        /**
         * DrawManageのリストから削除する
         */
        public void removeFromDrawManager()
        {
            UDrawManager.getInstance().removeDrawable(this);
        }


        /**
         * 移動
         * @param moveX
         * @param moveY
         */
        public void move(float moveX, float moveY)
        {
            pos.X += moveX;
            pos.Y += moveY;
            updateRect();
        }

        /**
         * startMovingの最初に呼ばれる処理
         * サブクラスでオーバーライドして使用する
         */
        public void startMoving()
        {
        }

        public void startMoving(float dstX, float dstY, int frame)
        {
            startMoving(MovingType.UniformMotion, dstX, dstY, frame);
        }

        /**
         * 自動移動(座標)
         * @param dstX  目的x
         * @param dstY  目的y
         * @param frame  移動にかかるフレーム数
         */
        public void startMoving(MovingType movingType, float dstX, float dstY, int frame)
        {
            if (!setMovingPos(dstX, dstY))
            {
                // 移動不要
                return;
            }
            startMoving();

            isMoving = true;
            isMovingPos = true;
            isMovingSize = false;
            this.movingType = movingType;
            movingFrame = 0;
            movingFrameMax = frame;
        }

        private bool setMovingPos(float dstX, float dstY)
        {
            if (pos.X == dstX && pos.Y == dstY)
            {
                return false;
            }
            srcPos.X = pos.X;
            srcPos.Y = pos.Y;
            dstPos.X = dstX;
            dstPos.Y = dstY;
            return true;
        }

        /**
         * 自動移動(サイズ)
         * @param dstW
         * @param dstH
         * @param frame
         */
        public void startMovingSize(int dstW, int dstH, int frame)
        {
            if (!setMovingSize(dstW, dstH))
            {
                // 移動不要
                return;
            }
            startMoving();

            movingType = MovingType.UniformMotion;
            isMoving = true;
            isMovingPos = false;
            isMovingSize = true;
            movingFrame = 0;
            movingFrameMax = frame;
        }

        private bool setMovingSize(int dstW, int dstH)
        {
            if (size.Width == dstW && size.Height == dstH)
            {
                return false;
            }
            srcSize.Width = size.Width;
            srcSize.Height = size.Height;
            dstSize.Width = dstW;
            dstSize.Height = dstH;
            return true;
        }

        /**
         * 自動移動(座標 & サイズ)
         * @param dstX
         * @param dstY
         * @param dstW
         * @param dstH
         * @param frame
         */
        public void startMoving(float dstX, float dstY, int dstW, int dstH, int frame)
        {
            startMoving(MovingType.UniformMotion, dstX, dstY, dstW, dstH, frame);
        }
        public void startMoving(MovingType movingType,
                                float dstX, float dstY, int dstW, int dstH, int frame)
        {
            bool noMoving = true;

            startMoving();

            if (setMovingPos(dstX, dstY))
            {
                noMoving = false;
            }
            if (setMovingSize(dstW, dstH))
            {
                noMoving = false;
            }
            if (!noMoving)
            {
                isMovingPos = true;
                isMovingSize = true;
                this.movingType = movingType;
                movingFrame = 0;
                movingFrameMax = frame;
                isMoving = true;
            }
        }

        /**
         * 移動、サイズ変更、色変更
         * 移動開始位置、終了位置、経過フレームから現在の値を計算する
         * @return true:移動中
         */
        public bool autoMoving()
        {
            if (!isMoving) return false;

            movingFrame++;
            if (movingFrame >= movingFrameMax)
            {
                // 移動完了
                if (isMovingPos)
                {
                    setPos(dstPos);
                }
                if (isMovingSize)
                {
                    setSize(dstSize.Width, dstSize.Height);
                }

                isMoving = false;
                isMovingPos = false;
                isMovingSize = false;

                updateRect();
                endMoving();
            }
            else
            {
                // 移動中
                // ratio 0.0(始点) -> 1.0(終点)
                float ratio = (float)movingFrame / (float)movingFrameMax;
                switch (movingType)
                {
                    case MovingType.UniformMotion:
                        break;
                    case MovingType.Acceleration:
                        ratio = UUtility.toAccel(ratio);
                        break;
                    case MovingType.Deceleration:
                        ratio = UUtility.toDecel(ratio);
                        break;
                }
                if (isMovingPos)
                {
                    setPos(srcPos.X + ((dstPos.X - srcPos.X) * ratio),
                            srcPos.Y + ((dstPos.Y - srcPos.Y) * ratio));
                }
                if (isMovingSize)
                {
                    setSize((int)(srcSize.Width + (dstSize.Width - srcSize.Width) * ratio),
                            (int)(srcSize.Height + (dstSize.Height - srcSize.Height) * ratio));
                }
            }
            return true;
        }

        /**
         * 自動移動完了時の処理
         */
        public void endMoving() { }

        /**
         * Drawableインターフェース
         */
        public void setDrawList(DrawList drawList)
        {
            this.drawList = drawList;
        }

        public DrawList getDrawList()
        {
            return drawList;
        }

        public int getDrawPriority()
        {
            return drawPriority;
        }

        public void setDrawPriority(int drawPriority)
        {
            this.drawPriority = drawPriority;
        }

        /**
         * 描画オフセットを取得する
         * @return
         */
        public PointF getDrawOffset()
        {
            return new PointF();
        }

        /**
         * アニメーション開始
         */
        public void startAnimation()
        {
            startAnimation(ANIME_FRAME);
        }
        public void startAnimation(int frameMax)
        {
            isAnimating = true;
            animeFrame = 0;
            animeFrameMax = frameMax;
            animeRatio = 0f;
        }

        /**
         * アニメーション終了時に呼ばれる処理
         */
        public void endAnimation()
        {
        }

        /**
         * アニメーション処理
         * といいつつフレームのカウンタを増やしているだけ
         * @return true:アニメーション中
         */
        public bool animate()
        {
            if (!isAnimating) return false;
            if (animeFrame >= animeFrameMax)
            {
                isAnimating = false;
                endAnimation();
            }
            else
            {
                animeFrame++;
                animeRatio = (float)animeFrame / (float)animeFrameMax;
            }
            return true;
        }

        
        /**
         * アニメーションフレームからアルファ値(1.0 -> 0.0 -> 1.0)を取得する
         *
         * @return
         */
        public int getAnimeAlpha()
        {
            double v1 = ((double)animeFrame / (double)animeFrameMax) * 180;
            return (int)((1.0 - Math.Sin(v1 * RAD)) * 255);
        }
    }

}
