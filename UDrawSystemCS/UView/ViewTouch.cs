using System;
using System.Drawing;
using UDrawSystemCS.UUtil;
using System.Windows.Forms;

namespace UDrawSystemCS.UView
{

    /**
     * UDrawに渡されるマウスのイベント
     */
    public enum MouseEvent
    {
        Down = 0,           // マウスのボタンが押された
        Up,                 // マウスのボタンが離された
        Click,              // マウスがクリックされた
        Cancel,             // マウスが領域外に出た
        Move,               // マウスが移動した
        Wheel               // マウスホイールが回転した
    }

    /**
     * Created by shutaro on 2017/06/14.
     * Viewのタッチの種類
     */
    public enum TouchType
    {
        None,
        Touch,        // タッチ開始(Mouse Down)
        //LongPress,    // 長押し
        Click,        // ただのクリック（タップ)
        //LongClick,    // 長クリック
        Moving,       // 移動
        MoveEnd,      // 移動終了
        MoveCancel    // 移動キャンセル
    }


    /**
     * View上のタッチ処理を判定する
     *
     */
    public class ViewTouch
    {
        public const String TAG = "ViewTouch";

        // クリック判定するためのタッチ座標誤差
        public const int CLICK_DISTANCE = 30;
        
        // 移動前の待機時間(sec)
        public const double MOVE_START_TIME = 0.1;

        public TouchType type;          // 外部用のタイプ(変化があった時に有効な値を返す)
        private TouchType innerType;    // 内部用のタイプ
        
        // タッチアップしたフレームだけtrueになる
        private bool _isTouchUp;

        public bool isTouchUp
        {
            get { return _isTouchUp; }
            set { _isTouchUp = value; }
        }


        private bool isTouching;
        private bool isLongTouch;

        // タッチ開始した座標
        private float _touchX;

        public float touchX
        {
            get { return _touchX; }
            set { _touchX = value; }
        }

        private float _touchY;

        public float touchY
        {
            get { return _touchY; }
            set { _touchY = value; }
        }

        protected float x, y;       // スクリーン座標
        float moveX, moveY;
        
        private bool _isMoveStart;

        public bool isMoveStart
        {
            get { return _isMoveStart; }
            set { _isMoveStart = value; }
        }


        // タッチ開始した時間
        double touchTime;

        /**
         * Get/Set
         */
        public float getX() { return x; }
        public float getY() { return y; }
        public float getX(float offset) { return x + offset; }
        public float getY(float offset) { return y + offset; }
        public float touchX2(float offset) { return this.touchX + offset; }
        public float touchY2(float offset) { return this.touchY + offset; }
        
        public void setTouching(bool touching)
        {
            isTouching = touching;
        }

        public float getMoveY()
        {
            return moveY;
        }

        public void setMoveY(float moveY)
        {
            this.moveY = moveY;
        }

        public float getMoveX()
        {
            return moveX;
        }

        public void setMoveX(float moveX)
        {
            this.moveX = moveX;
        }

        public ViewTouch()
        {
            //this(null);
            innerType = TouchType.None;
        }

        /**
         * ロングタッチがあったかどうかを取得する
         * このメソッドを呼ぶと内部のフラグをクリア
         * @return true:ロングタッチ
         */
        public bool checkLongTouch()
        {
            // ロングタッチが検出済みならそれを返す
            if (isLongTouch)
            {
                ULog.print(TAG, "Long Touch");
                isLongTouch = false;
                return true;
            }
            return false;
        }

        public TouchType checkTouchType(MouseEvent e, MouseEventArgs args)
        {
            isTouchUp = false;

            switch (e)
            {
                case MouseEvent.Down:
                    {
                        ULog.print(TAG, "Touch Down:" + args.Button);

                        isTouching = true;
                        touchX = args.Location.X;
                        touchY = args.Location.Y;
                        type = innerType = TouchType.Touch;
                        touchTime = TimeCountByPerformanceCounter.GetSecTime();
                    }
                    break;
                case MouseEvent.Up:
                case MouseEvent.Cancel:
                    {
                        if (args != null)
                        {
                            ULog.print(TAG, "Up:" + args.Button);
                        }
                        
                        isTouchUp = true;
                        if (isTouching)
                        {
                            if (innerType == TouchType.Moving)
                            {
                                ULog.print(TAG, "MoveEnd");
                                type = innerType = TouchType.MoveEnd;
                                return type;
                            }
                        }
                        else
                        {
                            type = TouchType.None;
                        }
                        isTouching = false;
                    }
                    break;
                case MouseEvent.Click:
                    type = TouchType.Click;
                    break;
                case MouseEvent.Move:
                    isMoveStart = false;

                    // クリックが判定できるようにタッチ時間が一定時間以上、かつ移動距離が一定時間以上で移動判定される
                    if (innerType != TouchType.Moving)
                    {
                        float dx = (args.Location.X - touchX);
                        float dy = (args.Location.Y - touchY);
                        float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                        Console.WriteLine("move");
                        if (dist >= CLICK_DISTANCE)
                        {
                            double time = TimeCountByPerformanceCounter.GetSecTime() - touchTime;
                            if (time >= MOVE_START_TIME)
                            {
                                type = innerType = TouchType.Moving;
                                isMoveStart = true;
                                x = touchX;
                                y = touchY;
                                ULog.print(TAG, "Move:" + x + "," + y);
                            }
                        }
                    }
                    if (innerType == TouchType.Moving)
                    {
                        moveX = args.Location.X - x;
                        moveY = args.Location.Y - y;
                    }
                    else
                    {
                        innerType = type = TouchType.None;
                    }
                    x = args.Location.X;
                    y = args.Location.Y;

                    break;
            }

            return type;
        }

        /**
         * ２点間の距離が指定の距離内に収まっているかどうかを調べる
         * @return true:距離内 / false:距離外
         */
        public bool checkInsideCircle(float vx, float vy, float x, float y, float length)
        {
            if ((vx - x) * (vx - x) + (vy - y) * (vy - y) <= length * length)
            {
                return true;
            }
            return false;
        }

    }
}
