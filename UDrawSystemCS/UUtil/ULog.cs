using System;
using System.Drawing;
using System.Collections.Generic;
using UDrawSystemCS.UView.Window;
using UDrawSystemCS.UView;

namespace UDrawSystemCS.UUtil
{
    /**
 * 出力を一括スイッチングできるLog
 * タグ毎のON/OFFを設定できる
 */
    public class ULog
    {

        /**
         * Constants
         */
        public const String TAG = "ULog";
        private const bool isEnable = true;  // マスターのスイッチ
        private const bool isCount = true;

        private const long NANO_TO_SEC = 1000000000;

        /**
         * Static variables
         */
        // タグ毎のON/OFF情報をMap(Dictionary)で持つ
        private static Dictionary<string, bool> enables = new Dictionary<string, bool>();
        private static Dictionary<string, int> counters = new Dictionary<string, int>();
        private static ULogWindow logWindow;
        private static double startTime;      // 初期時間（システムの時間からこの時間を引いて表示する)

        /**
         * Get/Set
         */
        // タグのON/OFFを設定する
        public static void setEnable(String tag, bool enable)
        {
            enables.Add(tag, enable);
        }
        public static void setLogWindow(ULogWindow _logWindow)
        {
            logWindow = _logWindow;
        }

        /**
         * Init
         */
        // 初期化、アプリ起動時に１回だけ呼ぶ
        public static void init()
        {
            setEnable(ViewTouch.TAG, true);
            //setEnable(UDrawManager.TAG, false);
            //setEnable(UMenuBar.TAG, false);
            //setEnable(UScrollBar.TAG, false);
            //setEnable(UButton.TAG, false);
            //setEnable(UWindow.TAG, false);
            
            startTime = TimeCountByPerformanceCounter.GetSecTime();
        }

        /**
         * 処理時間計測用のシステムの時間を初期化する
         * 以後の時間はこの時間からのどれだけ経過したかで表示される
         */
        public static void initSystemTime()
        {
            startTime = TimeCountByPerformanceCounter.GetSecTime();
        }

        // ログ出力
        public static void print(String tag, String msg)
        {
            if (!isEnable)
            {
                return;
            }

            // 有効無効判定
            if (enables.ContainsKey(tag))
            {
                bool enable = enables[tag];
                if (!enable)
                {
                    // 出力しない
                }
                else
                {
                    // 時間
                    double time = TimeCountByPerformanceCounter.GetSecTime();

                    Console.WriteLine(string.Format("{0}: {1}:{2}", time, tag, msg));

                    if (logWindow != null)
                    {
                        // todo
                        //logWindow.addLog(msg);
                    }
                }
            }
        }
        

        /**
         * Static Methods
         */
        public static void showRect(Rectangle rect)
        {
            ULog.print(TAG, "Rect left:" + rect.Left + " top:" + rect.Top +
                        " right:" + rect.Right + " bottom:" + rect.Bottom);
        }

        public static void showRectF(Rectangle rect)
        {
            ULog.print(TAG, "Rect left:" + rect.Left + " top:" + rect.Top +
                    " right:" + rect.Right + " bottom:" + rect.Bottom);
        }

    }

}
