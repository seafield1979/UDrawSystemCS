using System;
using System.Drawing;
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
     * View上のタッチ処理を判定する
     *
     */
    public class ViewTouch
    {
        public const String TAG = "ViewTouch";

        private MouseEvent mevent;

        // クリック時のマウス座標
        private Point clickPos;
        public Point ClickPos
        {
            get { return clickPos; }
            set { clickPos = value; }
        }


        public MouseEvent MEvent
        {
            get { return mevent; }
            set { mevent = value; }
        }

        public MouseEventArgs args;

    }
}
