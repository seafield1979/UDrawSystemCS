using System.Runtime.InteropServices;

namespace UDrawSystemCS.UUtil
{
    public class TimeCountByPerformanceCounter
    {
        [DllImport("kernel32.dll")]
        static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
        [DllImport("kernel32.dll")]
        static extern bool QueryPerformanceFrequency(ref long lpFrequency);

        private static long startCounter;

        public static void Start()
        {
            QueryPerformanceCounter(ref startCounter);
        }

        public static double GetSecTime()
        {
            long stopCounter = 0;
            QueryPerformanceCounter(ref stopCounter);
            long frequency = 0;
            QueryPerformanceFrequency(ref frequency);

            return (double)(stopCounter - startCounter) / frequency;
        }

        public static string Format()
        {
            return "QueryPerformanceCounter/Frequency..{0}ms";
        }
    }
}
