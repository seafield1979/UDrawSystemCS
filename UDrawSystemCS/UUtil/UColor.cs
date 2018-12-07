using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UDrawSystemCS.UUtil
{
    /**
 *　カスタマイズしたColorクラス
 */

    public class UColor
    {
        /**
         * ランダムな色を取得
         * @return
         */
        public static int getRandomColor()
        {
            Random rand = new Random();
            return (0xff << 24) | ((rand.Next() % 256) << 16) | ((rand.Next() % 256) << 8) | ((rand.Next() % 256));
        }

        /**
         * 文字列に変換する
         * @param color 変換元の色
         * @return 変換後の文字列 (#xxxxxx形式)
         */
        public static String toColorString(int color)
        {
            return string.Format("{0:X6}", color);
        }

        /**
         * RGB -> YUV に変換
         */
        public static int RGBtoYUV(int rgb)
        {
            float R = (rgb & 0xff0000) >> 16;
            float G = (rgb & 0x00ff00) >> 8;
            float B = rgb & 0x0000ff;

            int Y = (int)(0.257 * R + 0.504 * G + 0.098 * B + 16);
            int Cb = (int)(-0.148 * R - 0.291 * G + 0.439 * B + 128);
            int Cr = (int)(0.439 * R - 0.368 * G - 0.071 * B + 128);

            if (Y > 255) Y = 255;
            if (Cb > 255) Cb = 255;
            if (Cr > 255) Cr = 255;

            return Y << 16 | Cb << 8 | Cr;
        }

        // 輝度(Y)を指定して色を変更する
        public static int colorWithY(int rgb, int y)
        {
            float R = rgb & 0xff0000;
            float G = rgb & 0x00ff00;
            float B = rgb & 0x0000ff;

            int Cb = (int)(-0.148 * R - 0.291 * G + 0.439 * B + 128);
            int Cr = (int)(0.439 * R - 0.368 * G - 0.071 * B + 128);

            if (Cb > 255) Cb = 255;
            if (Cr > 255) Cr = 255;

            return YUVtoRGB(y << 16 | Cb << 8 | Cr);
        }

        public static int RGBtoY(int rgb)
        {
            float R = rgb & 0xff0000;
            float G = rgb & 0x00ff00;
            float B = rgb & 0x0000ff;

            return (int)(0.257 * R + 0.504 * G + 0.098 * B + 16);
        }


        /**
         * YUV -> RGB
         */
        public static int YUVtoRGB(int yuv)
        {
            float Y = (float)((yuv & 0xff0000) >> 16);
            float Cb = (float)((yuv & 0x00ff00) >> 8);
            float Cr = (float)(yuv & 0xff);

            int R = (int)(1.164 * (Y - 16) + 1.596 * (Cr - 128));
            int G = (int)(1.164 * (Y - 16) - 0.391 * (Cb - 128) - 0.813 * (Cr - 128));
            int B = (int)(1.164 * (Y - 16) + 2.018 * (Cb - 128));

            if (R > 255) R = 255;
            if (R < 0) R = 0;
            if (G > 255) G = 255;
            if (G < 0) G = 0;
            if (B > 255) B = 255;
            if (B < 0) B = 0;

            return R << 16 | G << 8 | B;
        }

        /**
         * 現在の色で輝度のみ変更する
         * @param argb
         * @param brightness 輝度 0:暗い ~ 255:明るい
         * @return
         */
        public static int setBrightness(int argb, int brightness)
        {
            int yuv = RGBtoYUV(argb);

            // 新しい色  元の色のアルファを維持する
            int _argb = (int)((argb & 0xff000000) | YUVtoRGB((brightness << 16) | (yuv & 0x00ffff)));
            return _argb;
        }

        /**
         * RGBの輝度を上げる
         * @param argb
         * @param addY  輝度 100% = 1.0 / 50% = 0.5
         * @return
         */
        public static int addBrightness(int argb, float addY)
        {
            int yuv = RGBtoYUV(argb);

            int Y = yuv >> 16;
            int Y2 = Y + (int)(addY * 255);
            if (Y2 > 255) Y2 = 255;
            else if (Y2 < 0) Y2 = 0;

            // 新しい色  元の色のアルファを維持する
            int _argb = (int)((argb & 0xff000000) | YUVtoRGB((Y2 << 16) | (yuv & 0x00ffff)));
            
            return _argb;
        }

        /**
         * 2つの色を指定の割合で合成する
         * @param color1
         * @param color2
         * @param ratio  合成比率 0.0 : color1=100%, colo2=0%
         *                      1.0 : color1=0%, color2=100%
         * @return
         */
        public static int mixRGBColor(int color1, int color2, float ratio)
        {
            int a = (int)(((color1 & 0xff000000) >> 24) * (1.0f - ratio) +
                    ((color2 & 0xff000000) >> 24) * ratio);
            if (a > 255) a = 255;
            int r = (int)(((color1 & 0xff0000) >> 16) * (1.0f - ratio) +
                    ((color2 & 0xff0000) >> 16) * ratio);
            if (r > 255) r = 255;
            int g = (int)(((color1 & 0xff00) >> 8) * (1.0f - ratio) +
                    ((color2 & 0xff00) >> 8) * ratio);
            if (g > 255) g = 255;
            int b = (int)((color1 & 0xff) * (1.0f - ratio) +
                    (color2 & 0xff) * ratio);
            if (b > 255) b = 255;

            return (a << 24) | (r << 16) | (g << 8) | b;
        }

        /**
         * colorを文字列(0xaarrggbb)に変換する
         * @param color
         * @return
         */
        public static String toString(int color)
        {
            int a = (color >> 24) & 0xff;
            int r = (color >> 16) & 0xff;
            int g = (color >> 8) & 0xff;
            int b = color & 0xff;

            return string.Format("0x{0:X2}{1:X2}{2:X2}{3:X2}", a, r, g, b);
        }
    }

}
