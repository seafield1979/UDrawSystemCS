﻿using System;
using System.Drawing;

namespace UDrawSystemCS.UUtil
{

    /**
     * Created by shutaro on 2016/12/07.
     *
     * 便利関数
     */


    public class UUtility
    {
        public const double RAD = 3.1415 / 180.0;

        /**
         * sinテーブルの0->90度の 0.0~1.0 の値を取得する
         *
         * @param ratio  0.0 ~ 1.0
         * @return 0.0 ~ 1.0
         */
        public static float toAccel(float ratio)
        {
            return (float)Math.Sin(ratio * 90 * RAD);
        }

        /**
         * 1.0 - cosテーブルの0->90度 の0.0~1.0の値を取得する
         * @param ratio
         * @return
         */
        public static float toDecel(float ratio)
        {
            return (float)(1.0 - Math.Cos(ratio * 90 * RAD));
        }

        /**
         * Bitmapをグレースケール（灰色）に変換する
         * @param bmp
         * @return
         */
        public static Bitmap convToGrayBitmap(Bitmap bmp)
        {
            // グレースケール変換
            //int height = bmp.Height;
            //int width = bmp.Width;
            //int size = height * width;
            //int[] pix = new int[size];
            //int pos = 0;

            //bmp.getPixels(pix, 0, width, 0, 0, width, height);
            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        int pixel = pix[pos];
            //        int red = (pixel & 0x00ff0000) >> 16;
            //        int green = (pixel & 0x0000ff00) >> 8;
            //        int blue = (pixel & 0x000000ff);
            //        int alpha = (pixel & 0xff000000) >> 24;
            //        int gray = (red + green + blue) / 3;
            //        pix[pos] = Color.argb(alpha, gray, gray, gray);
            //        pos++;
            //    }
            //}
            //Bitmap newBmp = Bitmap.createBitmap(pix, 0, width, width, height,
            //        Bitmap.Config.ARGB_8888);

            //return newBmp;
            return null;
        }


        /**
         * 単色Bitmap画像の色を変更する
         * 元の画像はグレースケール限定
         */
        public static Bitmap convBitmapColor(Bitmap bmp, int newColor)
        {
            // グレースケール変換
            //int height = bmp.getHeight();
            //int width = bmp.getWidth();
            //int size = height * width;
            //int pix[] = new int[size];
            //int pos = 0;
            //int[] colorConvTbl = new int[256];

            //bmp.getPixels(pix, 0, width, 0, 0, width, height);
            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        int pixel = pix[pos];
            //        int alpha = pixel & 0xff000000;
            //        // 白はそのまま
            //        if ((pixel & 0xffffff) == 0xffffff)
            //        {
            //            pix[pos] = pixel;
            //        }
            //        else
            //        {
            //            // 輝度(明るさ)を元に新しい色を求める。すでに同じ輝度で計算していたら結果をテーブルから取得する
            //            int _y = UColor.RGBtoY(pixel);
            //            if (pixel != 0 && colorConvTbl[_y] == 0)
            //            {
            //                colorConvTbl[_y] = UColor.colorWithY(newColor,
            //                        _y);
            //            }
            //            pix[pos] = alpha | colorConvTbl[_y];
            //        }
            //        pos++;
            //    }
            //}
            //Bitmap newBmp = Bitmap.createBitmap(pix, 0, width, width, height,
            //        Bitmap.Config.ARGB_8888);

            //return newBmp;
            return null;
        }
    }

}
