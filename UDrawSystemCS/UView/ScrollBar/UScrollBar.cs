using System;
using System.Drawing;
using UDrawSystemCS.UUtil;

namespace UDrawSystemCS.UView.ScrollBar
{
    /**
     * Created by shutaro on 2017/06/14.
     * スクロールバーの表示タイプ
     */
    public enum ScrollBarShowType
    {
        Show,           // 必要なら表示
        Show2,          // 必要なら表示（自動で非表示になる）
        ShowAllways     // 常に表示
    }

    /**
     * Created by shutaro on 2017/06/14.
     * スクロールバーの配置場所
     */

    public enum ScrollBarType
    {
        Horizontal,
        Vertical
    }



    /**
 * 自前で描画するスクロールバー
 * タッチ操作あり
 *
 * 機能
 *  外部の値に連動してスクロール位置を画面に表示
 *  ドラッグしてスクロール
 *  バー以外の領域をタップしてスクロール
 *  指定のViewに張り付くように配置
 */

    public class UScrollBar
    {
        /**
         * Constants
         */
        public const String TAG = "UScrollBar";

        private const int TOUCH_MARGIN = 40;     // バーは細くてタッチしにくいので見た目よりもあたり判定を広くするためのマージンを設定する1

        // colors
        private Color BAR_COLOR = Color.FromArgb(160, 128, 128, 128);
        private Color SHOW_BAR_COLOR = Color.FromArgb(255, 255, 128, 0);
        private Color SHOW_BG_COLOR = Color.FromArgb(128, 255, 255, 255);

        /**
         * Membar Variables
         */
        private ScrollBarType type;
        private ScrollBarShowType showType;
        private bool _isShow;

        public PointF pos = new PointF();
        public PointF parentPos;
        public Color bgColor, barColor;
        private bool isDraging;

        // スクリーン座標系
        public int bgLength, bgWidth;
        public float barPos;        // バーの座標（縦ならy,横ならx)
        public int barLength;       // バーの長さ(縦バーなら高さ、横バーなら幅)

        // コンテンツ座標系
        public long contentLen;       // コンテンツ領域のサイズ
        public long pageLen;          // 表示画面のサイズ
        public long topPos;         // スクロールの現在の位置

        protected RectangleF bgRect = new RectangleF();
        protected RectangleF barRect = new RectangleF();

        /**
         * Get/Set
         */

        public void setBgLength(int bgLength)
        {
            this.bgLength = bgLength;
        }

        public void setBgColor(Color bgColor)
        {
            this.bgColor = bgColor;
        }

        public void setBarColor(Color barColor)
        {
            this.barColor = barColor;
        }

        public long getTopPos()
        {
            return topPos;
        }

        private void updateBarLength()
        {
            if (pageLen >= contentLen)
            {
                // 表示領域よりコンテンツの領域が小さいので表示不要
                barLength = 0;
                topPos = 0;
                if (showType != ScrollBarShowType.ShowAllways)
                {
                    _isShow = false;
                }
            }
            else
            {
                barLength = (int)(this.bgLength * ((float)pageLen / (float)contentLen));
                _isShow = true;
            }
        }

        public int getBgWidth()
        {
            return bgWidth;
        }

        public void setPageLen(long pageLen)
        {
            this.pageLen = pageLen;
        }

        public bool isShow()
        {
            if (_isShow && barLength > 0)
            {
                return true;
            }
            return false;
        }

        public void setShow(bool show)
        {
            _isShow = show;
        }

        /**
         * コンストラクタ
         * 指定のViewに張り付くタイプのスクロールバーを作成
         *
         * @param pageLen   1ページ分のコンテンツの長さ
         * @param contentLen  全体のコンテンツの長さ
         */
        public UScrollBar(ScrollBarType type, ScrollBarShowType showType,
                          PointF paretnPos, float x, float y,
                          int bgLength, int bgWidth,
                          long pageLen, long contentLen)
        {
            this.type = type;
            this.showType = showType;
            if (showType == ScrollBarShowType.ShowAllways)
            {
                _isShow = true;
            }
            pos.X = x;
            pos.Y = y;
            topPos = 0;
            barPos = 0;
            this.parentPos = paretnPos;
            this.bgWidth = bgWidth;
            this.bgLength = bgLength;
            this.contentLen = contentLen;
            this.pageLen = pageLen;

            updateBarLength();

            if (showType == ScrollBarShowType.Show2)
            {
                bgColor = Color.Black;
                barColor = BAR_COLOR;
            }
            else
            {
                bgColor = SHOW_BAR_COLOR;
                barColor = SHOW_BG_COLOR;
            }

            updateSize();
        }

        /**
         * スクロールバーを表示する先のViewのサイズが変更された時の処理
         */
        public void updateSize()
        {
            updateBarLength();
            if (barPos + barLength > bgLength)
            {
                barPos = bgLength - barLength;
            }
        }


        /**
         * 色を設定
         * @param bgColor  背景色
         * @param barColor バーの色
         */
        public void setColor(Color bgColor, Color barColor)
        {
            this.bgColor = bgColor;
            this.barColor = barColor;
        }

        /**
         * 領域がスクロールした時の処理
         * ※外部のスクロールを反映させる
         * @param pos
         */
        public void updateScroll(long pos)
        {
            barPos = (pos / (float)contentLen) * bgLength;
            this.topPos = pos;
        }

        public void updateBarPos()
        {
            barPos = (topPos / (float)contentLen) * bgLength;
        }

        /**
         * バーの座標からスクロール量を求める
         * updateScrollの逆バージョン
         */
        private void updateScrollByBarPos()
        {
            topPos = (long)((barPos / bgLength) * contentLen);
        }

        /**
         * コンテンツやViewのサイズが変更された時の処理
         */
        public long updateContent(long contentSize)
        {
            this.contentLen = contentSize;

            updateBarLength();
            return topPos;
        }

        public void draw(Graphics g, PointF offset)
        {
            if (!_isShow) return;

            // todo
            //paint.setStyle(Paint.Style.FILL);

            //float baseX = pos.x + parentPos.x;
            //float baseY = pos.y + parentPos.y;
            //if (offset != null)
            //{
            //    baseX += offset.x;
            //    baseY += offset.y;
            //}

            //float _barLength = barLength;
            //float _barPos = barPos;
            //if (showType == ScrollBarShowType.ShowAllways)
            //{
            //    _barLength = bgLength - 30;
            //    _barPos = 15;
            //}
            //if (type == ScrollBarType.Horizontal)
            //{
            //    if (bgColor != 0)
            //    {
            //        bgRect.left = baseX;
            //        bgRect.right = baseX + bgLength;
            //        bgRect.top = baseY;
            //        bgRect.bottom = baseY + bgWidth;
            //    }

            //    barRect.left = baseX + _barPos;
            //    barRect.top = baseY + 10;
            //    barRect.right = baseX + _barPos + _barLength;
            //    barRect.bottom = baseY + bgWidth - 10;
            //}
            //else
            //{
            //    if (bgColor != 0)
            //    {
            //        bgRect.left = baseX;
            //        bgRect.top = baseY;
            //        bgRect.right = baseX + bgWidth;
            //        bgRect.bottom = baseY + bgLength;
            //    }

            //    barRect.left = baseX + 10;
            //    barRect.top = baseY + _barPos;
            //    barRect.right = baseX + bgWidth - 10;
            //    barRect.bottom = baseY + _barPos + _barLength;
            //}

            //// 背景
            //if (bgColor != 0)
            //{
            //    paint.setStyle(Paint.Style.FILL);
            //    paint.setColor(bgColor);
            //    canvas.drawRect(bgRect.left,
            //            bgRect.top,
            //            bgRect.right,
            //            bgRect.bottom,
            //            paint);
            //}

            //// バー
            //paint.setStyle(Paint.Style.FILL);
            //paint.setColor(barColor);
            //canvas.drawRect(barRect.left,
            //        barRect.top,
            //        barRect.right,
            //        barRect.bottom,
            //        paint);
        }


        /**
         * １画面分上（前）にスクロール
         */
        public void scrollUp()
        {
            topPos -= pageLen;
            if (topPos < 0)
            {
                topPos = 0;
            }
            updateBarPos();
        }

        /**
         * １画面分下（先）にスクロール
         */
        public void scrollDown()
        {
            topPos += pageLen;
            if (topPos + pageLen > contentLen)
            {
                topPos = contentLen - pageLen;
            }
            updateBarPos();
        }

        /**
         * バーを移動
         * @param move 移動量
         */
        public void barMove(float move)
        {
            barPos += move;
            if (barPos < 0)
            {
                barPos = 0;
            }
            else if (barPos + barLength > bgLength)
            {
                barPos = bgLength - barLength;
            }

            updateScrollByBarPos();
        }

        /**
         * タッチ系の処理
         * @param tv
         * @return
         */
        public bool touchEvent(ViewTouch tv, PointF offset)
        {
            switch (tv.MEvent)
            {
                case MouseEvent.Down:
                    if (touchDown(tv, offset))
                    {
                        return true;
                    }
                    break;
                case MouseEvent.Move:
                    if (touchMove(tv))
                    {
                        return true;
                    }
                    break;
                case MouseEvent.Up:
                    touchUp();
                    break;
            }
            return false;
        }

        /**
         * スクロールバーのタッチ処理
         * @param vt
         * @return true:バーがスクロールした
         */
        private bool touchDown(ViewTouch vt, PointF offset)
        {
            // スペース部分をタッチしたら１画面分スクロール
            //float ex = vt.touchX - offset.X;
            //float ey = vt.touchY - offset.Y;

            //RectangleF rect;
            //if (type == ScrollBarType.Vertical)
            //{
            //    rect = new RectangleF(pos.X - TOUCH_MARGIN, pos.Y,
            //                pos.X + bgWidth + TOUCH_MARGIN, pos.Y + bgLength);
            //    if (rect.Left <= ex && ex < rect.Right &&
            //            rect.Top <= ey && ey < rect.Bottom)
            //    {
            //        if (ey < barPos)
            //        {
            //            // 上にスクロール
            //            //ULog.print(TAG, "Scroll Up");
            //            scrollUp();
            //            return true;
            //        }
            //        else if (ey > pos.Y + barPos + barLength)
            //        {
            //            // 下にスクロール
            //            //ULog.print(TAG, "Scroll Down");
            //            scrollDown();
            //            return true;
            //        }
            //        else
            //        {
            //            // バー
            //            //ULog.print(TAG, "Drag Start");
            //            isDraging = true;
            //            return true;
            //        }
            //    }
            //}
            //else
            //{
            //    rect = new RectangleF(pos.X, pos.Y - TOUCH_MARGIN,
            //            pos.X + bgLength, pos.Y + bgWidth);

            //    if (rect.Left <= ex && ex < rect.Right &&
            //            rect.Top <= ey && ey < rect.Bottom)
            //    {
            //        if (ex < barPos)
            //        {
            //            // 上にスクロール
            //            ULog.print(TAG, "Scroll Up");
            //            scrollUp();
            //            return true;
            //        }
            //        else if (ex > pos.X + barPos + barLength)
            //        {
            //            // 下にスクロール
            //            ULog.print(TAG, "Scroll Down");
            //            scrollDown();
            //            return true;
            //        }
            //        else
            //        {
            //            // バー
            //            ULog.print(TAG, "Drag Start");
            //            isDraging = true;
            //            return true;
            //        }
            //    }
            //}
            return false;
        }

        private bool touchUp()
        {
            ULog.print(TAG, "touchUp");
            isDraging = false;

            return false;
        }

        private bool touchMove(ViewTouch vt)
        {
            //if (isDraging)
            //{
            //    float move = (type == ScrollBarType.Vertical) ? vt.getMoveY() : vt.getMoveX();
            //    barMove(move);
            //    return true;
            //}
            return false;
        }
    }

}
