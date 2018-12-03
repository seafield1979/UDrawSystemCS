using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UDrawSystemCS.UDraw
{
    /*
     * 拡大率を管理するクラス
     */
    public class ZoomRate
    {
        enum EZoomRate : byte
        {
            E50P = 0,
            E67P,
            E75P,
            E80P,
            E90P,
            E100P,  // 100%
            E110P,
            E125P,
            E150P,
            E175P,
            E200P,
            E250P,
            E300P,
            E400P
        }
        private EZoomRate zoomRate;

        private float _value;

        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }

        //
        // Consts
        //
        private float[] eToV = new float[] { 0.5f, 0.67f, 0.75f, 0.8f, 0.9f, 1.0f, 1.1f, 1.25f, 1.5f, 1.75f, 2.0f, 2.5f, 3.0f, 4.0f };

        //
        // Constructor
        // 
        public ZoomRate()
        {
            zoomRate = EZoomRate.E100P;
            SetZoomValue();
        }

        private void SetZoomValue()
        {
            _value = eToV[(byte)zoomRate];
        }

        public float ZoomIn()
        {
            if (zoomRate < EZoomRate.E400P)
            {
                zoomRate++;
            }
            SetZoomValue();
            return _value;
        }

        public float ZoomOut()
        {
            if (zoomRate > EZoomRate.E50P)
            {
                zoomRate--;
            }
            SetZoomValue();
            return _value;
        }
    }

    /// <summary>
    /// フォント管理クラス
    /// </summary>
    public class UFontManager
    {
        //
        // Property
        //
        private static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

        // 
        // Methods
        // 

        /// <summary>
        /// フォントを取得する。過去に作成したフォントをストックしておく。
        /// </summary>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static Font getFont(string fontName, int fontSize)
        {
            string key = fontName + ":" + fontSize;
            if (fonts.ContainsKey(key) == false)
            {
                Font font = new Font(fontName, fontSize);
                fonts[key] = font;
                return font;
            }
            else
            {
                return fonts[key];
            }
        }
    }

    public class UColorManager
    {
        // 
        // Property
        //
        private static Dictionary<int, Color> colors = new Dictionary<int, Color>();

        //
        // Methods
        //

        /// <summary>
        /// Get color from alpha,r,g,b
        /// </summary>
        /// <returns></returns>
        public static Color getColor(int alpha, int r, int g, int b)
        {
            int key = (alpha << 24) | (r << 16) | (g << 8) | b;

            if (colors.ContainsKey(key) == false)
            {
                colors[key] = Color.FromArgb(alpha, r, g, b);
            }
            
            return colors[key];
        }

        /// <summary>
        /// Get color from ARGB
        /// </summary>
        /// <param name="argb"></param>
        /// <returns></returns>
        public static Color getColor(int argb)
        {
            if (colors.ContainsKey(argb) == false)
            {
                colors[argb] = Color.FromArgb(argb);
            }

            return colors[argb];
        }
    }

    public class UBrushManager
    {
        //
        // Properties
        // 
        private static Dictionary<int, Brush> brushes = new Dictionary<int, Brush>();

        // 
        // Methods
        //
       
        public static Brush getBrush(int argb)
        {
            if (brushes.ContainsKey(argb) == false)
            {
                brushes[argb] = new SolidBrush(UColorManager.getColor(argb));
            }

            return brushes[argb];
        }

        public static Brush getBrush(Color color)
        {
            int argb = color.ToArgb();
            if (brushes.ContainsKey(argb) == false)
            {
                brushes[argb] = new SolidBrush(UColorManager.getColor(argb));
            }

            return brushes[argb];
        }
    }

    /// <summary>
    /// 描画に使用する様々な機能を提供する
    /// </summary>
    public class UDrawUtility
    {
        //
        // Property
        // 

        #region Property
        // 全体のズーム率
        public static ZoomRate zoomRate;

        public static string FontName = "MS UI Gothic";

        #endregion Property

        /// <summary>
        /// アプリのデフォルトのフォントを取得する
        /// </summary>
        /// <param name="name"></param>
        public static void setFontName(string name)
        {
            FontName = name;
        }
        

        // 画面全体のズーム率を設定する
        public static void setZoomRate(ZoomRate _zoomRate)
        {
            zoomRate = _zoomRate;
        }

        // 指定の整数値にズーム率をかけた結果を取得
        public static int getZoomValue(int value)
        {
            return (int)(value * zoomRate.Value);
        }

        // 指定の浮動小数値にズーム率をかけた結果を取得
        public static float getZoomValue(float value)
        {
            return (float)(value * zoomRate.Value);
        }


        // 範囲チェック
        public static bool checkInside(PointF pos, Rectangle rect)
        {
            if (rect.Left <= pos.X && pos.X <= rect.Right &&
                rect.Top <= pos.Y && pos.Y <= rect.Bottom)
            {
                return true;
            }
            return false;
        }

        public static bool checkInside(PointF pos, int x, int y, int width, int height)
        {
            if (x <= pos.X && pos.X <= x + width &&
                y <= pos.Y && pos.Y <= y + height)
            {
                return true;
            }
            return false;
        }
    }
}
