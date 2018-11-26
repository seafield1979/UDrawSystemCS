using System;
using System.Collections.Generic;
using System.Drawing;
using UDrawSystemCS.UUtil;
using UDrawSystemCS.UView;
using System.Linq;
using System.Windows.Forms;

namespace UDrawSystemCS.UDraw
{
    /**
     * デバッグ座標の１点の情報
     */
    class DebugPoint
    {
        public float x, y;
        public Color color;
        public bool drawText;

        public DebugPoint(float x, float y, Color color, bool drawText)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.drawText = drawText;
        }
    }

    public class DrawList
    {
        // 描画範囲 この範囲外には描画しない
        private int priority;
        private List<UDrawable> list = new List<UDrawable>();

        public DrawList(int priority)
        {
            this.priority = priority;
        }

        // Get/Set
        public int getPriority()
        {
            return priority;
        }

        /**
         * リストに追加
         * すでにリストにあった場合は末尾に移動
         * @param obj
         */
        public void add(UDrawable obj)
        {
            list.Remove(obj);
            list.Add(obj);
        }

        public bool remove(UDrawable obj)
        {
            return list.Remove(obj);
        }

        public void toLast(UDrawable obj)
        {
            list.Remove(obj);
            list.Add(obj);
        }

        /**
         * Is contain in list
         * @param obj
         * @return
         */
        public bool contains(UDrawable obj)
        {
            foreach (UDrawable _obj in list)
            {
                if (obj == _obj)
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * リストの描画オブジェクトを描画する
         * @param canvas
         * @param paint
         * @return true:再描画あり (まだアニメーション中のオブジェクトあり)
         */
        public bool draw(Graphics g)
        {
            // 分けるのが面倒なのでアニメーションと描画を同時に処理する
            bool allDone = true;
            foreach (UDrawable obj in list)
            {

                if (obj.animate())
                {
                    allDone = false;
                }
                PointF offset = obj.getDrawOffset();
                obj.draw(g, offset);
                drawId(g, obj.getRect(), priority);
            }
            return !allDone;
        }

        /**
         * 毎フレームの処理
         * @return
         */
        public DoActionRet doAction()
        {

            DoActionRet ret = DoActionRet.None;
            foreach (UDrawable obj in list)
            {
                DoActionRet _ret = obj.doAction();
                switch (_ret)
                {
                    case DoActionRet.Done:
                        return _ret;
                    case DoActionRet.Redraw:
                        ret = _ret;
                        break;
                }
            }
            return ret;
        }

        /**
         * プライオリティを表示する
         * @param canvas
         * @param paint
         */
        protected void drawId(Graphics g, Rectangle rect, int priority)
        {
            // idを表示
            //if (!UDebug.drawIconId) return;

            //paint.setColor(Color.BLACK);
            //paint.setTextSize(30);

            //String text = "" + priority;
            //Rect textRect = new Rect();
            //paint.getTextBounds(text, 0, text.length(), textRect);

            //canvas.drawText("" + priority, rect.centerX() - textRect.width() / 2, rect.centerY() - textRect.height() / 2, paint);

        }

        /**
         * タッチアップイベント処理
         * @param vt
         * @return
         */
        public bool touchUpEvent(ViewTouch vt)
        {
            bool isRedraw = false;

            foreach (UDrawable obj in list)
            {
                if (obj.touchUpEvent(vt))
                {
                    isRedraw = true;
                }
            }
            return isRedraw;
        }

        /**
         * タッチイベント処理
         * リストの末尾(手前に表示されている)から順に処理する
         * @param vt
         * @return true:再描画
         */
        public bool touchEvent(ViewTouch vt)
        {
            UDrawManager manager = UDrawManager.getInstance();
            bool ret = false;

            // 手前に表示されたものから処理したいのでリストを逆順で処理する
            list.Reverse();
            foreach ( UDrawable obj in list)
            {
                if (!obj.isShow)
                {
                    continue;
                }
                PointF offset = obj.getDrawOffset();

                if (obj.touchEvent(vt, offset))
                {
                    ret = true;
                    break;
                }
            }
            list.Reverse();     // 逆順を元に戻す
            return ret;
        }


        /**
         * for Debug
         */
        /**
         * 描画オブジェクトをすべて出力する
         * @param isShowOnly  画面に表示中のもののみログを出力する
         */
        public void showAll(bool ascending, bool isShowOnly)
        {   
            if (!ascending) {
                list.Reverse();
            }
            foreach (UDrawable obj in list)
            {
                if (!isShowOnly || obj.isShow)
                {
                    String objStr = obj.ToString();
                    ULog.print(UDrawManager.TAG, objStr + " isShow:" + obj.isShow);
                }
            }
            if (!ascending)
            {
                list.Reverse();
            }
        }
    }


    class UDrawManager
    {
        /**
     * Constants
     */
        public const String TAG = "UDrawManager";
        private const int DEFAULT_PAGE = 1;

        /**
         * Static variables
         */
        private static UDrawManager singleton = new UDrawManager();
        public static UDrawManager getInstance() { return singleton; }

        // デバッグ用のポイント描画
        private static LinkedList<DebugPoint> debugPoints = new LinkedList<DebugPoint>();
        private static Dictionary<int, DebugPoint> debugPoints2 = new Dictionary<int, DebugPoint>();

        /**
         * Member variable
         */
        // タッチ中のDrawableオブジェクト
        // タッチを放すまで他のオブジェクトのタッチ処理はしない
        private UDrawable touchingObj;

        // ページのリスト
        //private Dictionary<int, Dictionary<int, DrawList>> mPageList;
        private Dictionary<int, DrawList> mDrawList;

        // カレントページ
        private int mCurrentPage = DEFAULT_PAGE;

        private LinkedList<UDrawable> removeRequest = new LinkedList<UDrawable>();

        /**
         * Get/Set
         */
        public UDrawable getTouchingObj()
        {
            return touchingObj;
        }

        public void setTouchingObj(UDrawable touchingObj)
        {
            this.touchingObj = touchingObj;
        }

        /**
         * 初期化
         * アクティビティが生成されるタイミングで呼ぶ
         */
        public void init()
        {
            mDrawList = new Dictionary<int, DrawList>();
        }
        
        public void initDrawList()
        {
            mDrawList.Clear();
        }
        
        /**
         * 描画オブジェクトを追加
         * @param obj
         * @return
         */
        public DrawList addWithNewPriority(UDrawable obj, int priority)
        {
            obj.drawPriority = priority;
            return addDrawable(obj);
        }
        public DrawList addDrawable(UDrawable obj)
        {
            // カレントページのリストを取得
            Dictionary<int, DrawList> lists = mDrawList;

            // 挿入するリストを探す
            int _priority = obj.getDrawPriority();
            DrawList list;
            if (lists.ContainsKey(_priority) == false)
            {
                // まだ存在していないのでリストを生成
                list = new DrawList(obj.getDrawPriority());
                lists.Add(_priority, list);
            }
            else
            {
                list = lists[_priority];
            }
            
            list.add(obj);
            obj.setDrawList(list);
            return list;
        }

        /**
         * リストに登録済みの描画オブジェクトを削除
         * 削除要求をバッファに積んでおき、描画前に削除チェックを行う
         * @param obj
         * @return
         */
        public void removeDrawable(UDrawable obj)
        {
            removeRequest.AddLast(obj);
        }

        /**
         * 削除要求のリストの描画オブジェクトを削除する
         */
        private void removeRequestedList()
        {
            Dictionary<int, DrawList> lists = mDrawList;
            if (lists == null) return;

            foreach (UDrawable obj in removeRequest)
            {
                int _priority = obj.getDrawPriority();
                DrawList list = lists[_priority];
                if (list != null)
                {
                    list.remove(obj);
                }
            }
            removeRequest.Clear();
        }

        /**
         * 指定のプライオリティのオブジェクトを全て削除
         * @param priority
         */
        public void removeWithPriority(int priority)
        {
            Dictionary<int, DrawList> lists = mDrawList;

            lists.Remove(priority);
        }

        /**
         * DrawListのプライオリティを変更する
         * @param list1  変更元のリスト
         * @param priority
         */
        public void setPriority(DrawList list1, int priority)
        {
            Dictionary<int, DrawList> lists = mDrawList;

            // 変更先のプライオリティーを持つリストを探す
            int _priority = priority;
            DrawList list2 = lists[_priority];
            if (list2 != null)
            {
                // すでに変更先のプライオリティーのリストがあるので交換
                int srcPriority = list1.getPriority();
                int _srcPriority = srcPriority;
                lists.Add(_priority, list1);
                lists.Add(_srcPriority, list2);
            }
            else
            {
                lists.Add(_priority, list1);
            }
        }

        /**
         * 追加済みのオブジェクトのプライオリティーを変更する
         * @param obj
         * @param priority
         */
        public void setPriority(UDrawable obj, int priority)
        {
            Dictionary<int, DrawList> lists = mDrawList;

            // 探す
            foreach (int pri in lists.Keys)
            {
                DrawList list = lists[pri];
                if (list.contains(obj))
                {
                    if (pri == priority)
                    {
                        // すでに同じPriorityにいたら末尾に移動
                        list.toLast(obj);
                    }
                    else
                    {
                        list.remove(obj);
                        addDrawable(obj);
                        return;
                    }
                }
            }
        }

        /**
         * 配下の描画オブジェクトを全て描画する
         * @param canvas
         * @param paint
         * @return true:再描画あり / false:再描画なし
         */
        public bool draw(Graphics g)
        {
            bool redraw = false;
            Dictionary<int, DrawList> lists = mDrawList;

            // 削除要求のかかったオブジェクトを削除する
            removeRequestedList();

            foreach (DrawList list in lists.Values)
            {
                // 毎フレームの処理
                DoActionRet ret = list.doAction();
                if (ret == DoActionRet.Done)
                {
                    redraw = true;
                    break;
                }
                else if (ret == DoActionRet.Redraw)
                {
                    redraw = true;
                }
            }


            // 奥から描画するので、キーが大きい（優先度が低い）順で描画（キーを降順で描画）
            List<int> _list = lists.Keys.ToList<int>();

            // 降順ソート
            _list.Sort((a, b) => b - a);

            foreach(int key in _list)
            {
                lists[key].draw(g);
            }

            drawDebugPoint(g);

            return redraw;
        }

        /**
         * タッチイベント処理
         * 描画優先度の高い順に処理を行う
         * @param vt
         * @return true:再描画
         */
        public bool touchEvent(ViewTouch vt)
        {
            Dictionary<int, DrawList> lists = mDrawList;

            bool isRedraw = false;
            
            //foreach (DrawList list in lists.Values)
            //{
            //    if (list.touchUpEvent(vt))
            //    {
            //        // タッチアップイベントは全てのオブジェクトで処理する
            //        isRedraw = true;
            //    }
            //}

            if (vt.MEvent == MouseEvent.Down)
            {
                foreach (DrawList list in lists.Values)
                {
                    if (list.touchEvent(vt))
                    {
                        // その他のタッチイベントはtrueが返った時点で打ち切り
                        return true;
                    }
                }
            }

            return isRedraw;
        }

        /**
         * 全ての描画オブジェクト情報を出力する
         */
        public void showAllList(bool ascending, bool isShowOnly)
        {
            // カレントページのリストを取得
            Dictionary<int, DrawList> lists = mDrawList;

            ULog.print(TAG, " ++ showAllList ++");

            IOrderedEnumerable<KeyValuePair<int, DrawList>> descendingList =
                lists.OrderByDescending(selector =>
                {
                    DrawList list = lists[selector.Key];

                    ULog.print(TAG, " + priority:" + list.getPriority());
                    list.showAll(ascending, isShowOnly);

                    return selector.Key;
                });
        }

        /**
         * デバッグ用の点を描画する
         */
        /**
         * 点の追加
         */
        // List
        public static void addDebugPoint(float x, float y, Color color, bool drawText)
        {
            debugPoints.AddLast(new DebugPoint(x, y, color, drawText));
        }

        // Map
        public static void setDebugPoint(int id, float x, float y, Color color, bool drawText)
        {
            debugPoints2.Add(id, new DebugPoint(x, y, color, drawText));
        }

        /**
         * 全てクリア
         */
        public static void clearDebugPoint()
        {
            debugPoints.Clear();
            debugPoints2.Clear();
        }

        private void drawDebugPoint(Graphics g)
        {
            foreach (DebugPoint dp in debugPoints)
            {
                UDraw.drawLine(g, dp.x - 50, dp.y, dp.x + 50, dp.y, 3, dp.color);
                UDraw.drawLine(g, dp.x, dp.y - 50, dp.x, dp.y + 50, 3, dp.color);
            }

            foreach (DebugPoint dp in debugPoints2.Values)
            {
                UDraw.drawLine(g, dp.x - 50, dp.y, dp.x + 50, dp.y, 3, dp.color);
                UDraw.drawLine(g, dp.x, dp.y - 50, dp.x, dp.y + 50, 3, dp.color);
            }
        }
    }
}
