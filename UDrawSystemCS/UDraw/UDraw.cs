using System;
using System.Collections.Generic;
using System.Drawing;

namespace UDrawSystemCS.UDraw
{
    /**
     * Created by shutaro on 2017/06/14.
     */
    public enum FontSize
    {
        S,
        M,
        L
    }

    /**
     * Created by shutaro on 2017/06/14.
     * 描画の位置揃え（アライメント)のタイプ
     */
    public enum UAlignment : int
    {
        None = 0,
        CenterX,
        CenterY,
        Center,
        Left,
        Right,
        Right_CenterY
    }

    /**
     * Created by shutaro on 2017/06/14.
     * doActionメソッドをの戻り値
     */
    public enum DoActionRet
    {
        None,               // 何も処理しない
        Redraw,             // 再描画あり(doActionループ処理を継続)
        Done                // 完了(doActionループ終了)
    }



    public class UDraw
    {
        // フォントのサイズ
        public static int getFontSize(FontSize size)
        {
            int _size;
            switch (size)
            {
                case FontSize.S:
                    _size = 10;
                    break;
                case FontSize.M:
                    _size = 13;
                    break;
                case FontSize.L:
                default:
                    _size = 17;
                    break;
            }
            return _size;
        }

        // ラジアン角度
        public const double RAD = 3.1415 / 180.0;

        public static void drawLine(Graphics g, float x1, float y1, float x2, float y2,
                                    int lineWidth, Color color)
        {
            // todo:
            //paint.setStyle(Paint.Style.STROKE);
            //paint.setStrokeWidth(lineWidth);
            //paint.setColor(color);
            //canvas.drawLine(x1, y1, x2, y2, paint);
        }

        /**
         * 矩形描画 (ライン）
         *
         * @param canvas
         * @param paint
         * @param rect
         * @param width  線の太さ
         * @param color
         */
        public static void drawRect(Graphics g, Rectangle rect, int width, int color)
        {
            //paint.setStyle(Paint.Style.STROKE);
            //paint.setStrokeWidth(width);
            //paint.setColor(color);
            //canvas.drawRect(rect, paint);
        }

        /**
         * 角丸矩形描画 (ライン）
         *
         * @param canvas
         * @param paint
         * @param rect
         * @param width  線の太さ
         * @param color
         */
        public static void drawRoundRect(Graphics g, RectangleF rect, int width,
                                         float radius, int color)
        {
            //paint.setStyle(Paint.Style.STROKE);
            //paint.setStrokeWidth(width);
            //paint.setColor(color);
            //canvas.drawRoundRect(rect, radius, radius, paint);
        }

        /**
         * 矩形描画(塗りつぶし)
         *
         * @param canvas
         * @param paint
         * @param rect
         * @param color
         */
        public static void drawRectFill(Graphics g, Rectangle rect, int color)
        {
            // todo
            //drawRectFill(canvas, paint, rect, color, 0, 0);
        }
        public static void drawRectFill(Graphics g, Rectangle rect, int color,
                                        int strokeWidth, int strokeColor)
        {
            //paint.setStyle(Paint.Style.FILL);
            //paint.setColor(color);
            //canvas.drawRect(rect, paint);

            //if (strokeWidth > 0)
            //{
            //    paint.setStyle(Paint.Style.STROKE);
            //    paint.setStrokeWidth(strokeWidth);
            //    paint.setColor(strokeColor);
            //    canvas.drawRect(rect, paint);
            //}
        }

        /**
         * 角丸四角形(塗りつぶし)
         *
         * @param canvas
         * @param paint
         * @param rect
         * @param strokeWidth
         * @param strokeColor
         * @param radius      角の半径
         * @param color
         */
        public static void drawRoundRectFill(Graphics g, RectangleF rect,
                                             float radius, Color color,
                                             int strokeWidth, Color strokeColor)
        {
            //paint.setStyle(Paint.Style.FILL);
            //paint.setColor(color);
            //canvas.drawRoundRect(rect, radius, radius, paint);

            //if (strokeWidth > 0)
            //{
            //    paint.setStyle(Paint.Style.STROKE);
            //    paint.setStrokeWidth(strokeWidth);
            //    paint.setColor(strokeColor);
            //    canvas.drawRoundRect(rect, radius, radius, paint);
            //}
        }

        /**
         * 円描画(線)
         *
         * @param canvas
         * @param paint
         * @param center
         * @param radius
         * @param width
         * @param color
         */
        public static void drawCircle(Graphics g, PointF center, float radius, int width, int color)
        {
            //paint.setStyle(Paint.Style.STROKE);
            //paint.setStrokeWidth(width);
            //paint.setColor(color);
            //canvas.drawCircle(center.x, center.y, radius, paint);
        }

        /**
         * 円描画(塗りつぶし)
         *
         * @param canvas
         * @param paint
         * @param center
         * @param radius
         * @param color
         */
        public static void drawCircleFill(Graphics g, PointF center, float radius, int color)
        {
            //paint.setStyle(Paint.Style.FILL);
            //paint.setColor(color);
            //canvas.drawCircle(center.x, center.y, radius, paint);
        }

        /**
         * Cross(×)を描画
         *
         * @param canvas
         * @param paint
         * @param center
         * @param radius
         * @param width
         * @param color
         */
        public static void drawCross(Graphics g, PointF center, float radius,
                                     int width, int color)
        {
            //paint.setColor(color);
            //paint.setStrokeWidth(width);
            //float x = (float)Math.cos(45 * RAD) * radius * 0.8f;
            //float y = (float)Math.sin(45 * RAD) * radius * 0.8f;
            //canvas.drawLine(center.x - x, center.y - y,
            //        center.x + x, center.y + y, paint);
            //canvas.drawLine(center.x - x, center.y + y,
            //        center.x + x, center.y - y, paint);
        }

        /**
         * 三角形描画(線)
         *
         * @param canvas
         * @param paint
         * @param center
         * @param radius
         * @param color
         */
        public static void drawTriangle(Graphics g, PointF center, float radius, int width, int color)
        {
            //Path path = trianglePath(center, radius, 0);

            //paint.setStyle(Paint.Style.STROKE);
            //paint.setColor(color);
            //paint.setStrokeWidth(width);
            //canvas.drawPath(path, paint);
        }

        /**
         * 三角形描画(塗りつぶし)
         *
         * @param canvas
         * @param paint
         * @param center
         * @param radius
         * @param color
         */
        public static void drawTriangleFill(Graphics g, PointF center,
                                            float radius, float rotate, int color)
        {
            //Path path = trianglePath(center, radius, rotate);

            //paint.setStyle(Paint.Style.FILL);
            //paint.setColor(color);
            //canvas.drawPath(path, paint);
        }

        private static PointF[] trianglePath(PointF center, float radius, float rotate)
        {
            // 中心から半径の位置にある３点で三角形を描画する
            Point p1, p2, p3;

            float baseAngle = 180 + rotate;
            float angle = baseAngle + 90;
            p1 = new Point((int)(Math.Cos(angle * RAD) * radius),
                    (int)(Math.Sin(angle * RAD) * radius));

            angle = baseAngle + 210;
            p2 = new Point((int)(Math.Cos(angle * RAD) * radius),
                    (int)(Math.Sin(angle * RAD) * radius));

            angle = baseAngle + 330;
            p3 = new Point((int)(Math.Cos(angle * RAD) * radius),
                    (int)(Math.Sin(angle * RAD) * radius));

            // 線を３つつなぐ
            PointF[] path = new PointF[4];
            path[0].X = p1.X + center.X;
            path[0].Y = p1.Y + center.Y;

            path[1].X = p2.X + center.X;
            path[1].Y = p2.Y + center.Y;

            path[2].X = p3.X + center.X;
            path[2].Y = p3.Y + center.Y;

            path[3].X = p1.X + center.X;
            path[3].Y = p1.Y + center.Y;
            
            return path;
        }


        /**
         * チェックボックスを描画する
         *
         * @param canvas
         * @param paint
         * @param isChecked
         * @param x
         * @param y
         * @param width
         * @param color     チェック時の色(みチェック時は灰色)
         */
        public static void drawCheckbox(Graphics g, bool isChecked,
                                        float x, float y, float width, int color)
        {
            //RectF rect = new RectF(x, y, x + width, y + width);

            //if (isChecked)
            //{
            //    // 枠
            //    drawRoundRectFill(canvas, paint, rect, UDpi.toPixel(3), color, 0, 0);

            //    // チェック
            //    Path path = new Path();
            //    paint.setStyle(Paint.Style.STROKE);
            //    path.moveTo(x + width * 0.2f, y + width * 0.4f);
            //    path.lineTo(x + width * 0.4f, y + width * 0.7f);
            //    path.lineTo(x + width * 0.75f, y + width * 0.25f);
            //    paint.setColor(Color.WHITE);
            //    paint.setStrokeWidth(UDpi.toPixel(2));
            //    canvas.drawPath(path, paint);

            //}
            //else
            //{
            //    // 枠
            //    drawRoundRect(canvas, paint, rect, UDpi.toPixel(3), UDpi.toPixel(5), Color.rgb(140, 140, 140));
            //}
        }

        // Bitmapで描画1
        public static void drawCheckboxImage(Graphics g, bool isChecked,
                                        float x, float y, float width, int color)
        {
            //if (isChecked)
            //{
            //    // 枠とチェック
            //    UDraw.drawBitmap(canvas, paint, UResourceManager.getBitmapWithColor(R.drawable.checked2, color), x, y, (int)width, (int)width);
            //}
            //else
            //{
            //    // 枠
            //    UDraw.drawBitmap(canvas, paint, UResourceManager.getBitmapById(R.drawable.checked3_frame), x, y, (int)width, (int)width);
            //}
        }

        /**
         * テキストを描画する（最初の１行のみ）
         *
         * @param canvas
         * @param paint
         * @param text
         * @param alignment
         * @param textSize
         * @return
         */
        public static Size drawTextOneLine(Graphics g, String text,
                                           UAlignment alignment, int textSize,
                                           float x, float y, Color color)
        {
            return drawTextOneLine(g, text,
                    alignment, textSize,
                    x, y, color, Color.Transparent, 0);
        }

        public static Size drawTextOneLine(Graphics g, String text,
                                           UAlignment alignment, int textSize,
                                           float x, float y, Color color, Color bgColor, int margin)
        {
            if (text == null) return new Size(0,0);

            // アンチエイリアス
            //paint.setAntiAlias(true);

            //// x,yにラインを表示 for Debug
            //if (UDebug.drawTextBaseLine)
            //{
            //    drawLine(canvas, paint, x - UDpi.toPixel(17), y, x + UDpi.toPixel(17), y, UDpi.toPixel(1), Color.YELLOW);
            //    drawLine(canvas, paint, x, y - UDpi.toPixel(17), x, y + UDpi.toPixel(17), UDpi.toPixel(1), Color.YELLOW);
            //}

            //int pos = text.indexOf("\n");
            //String _text;
            //if (pos != -1)
            //{
            //    _text = text.substring(0, pos);
            //}
            //else
            //{
            //    _text = text;
            //}

            //paint.setStyle(Paint.Style.FILL);
            //paint.setStrokeWidth(1);
            //paint.setTextSize(textSize);

            //int width = (int)paint.measureText(text);
            //Paint.FontMetrics fontMetrics = paint.getFontMetrics();

            //// テキストの左上端がx,yと一致するように補正
            //// テキストの左上端がx,yと一致するように補正
            //switch (alignment)
            //{
            //    case None:
            //        y -= fontMetrics.ascent;
            //        break;
            //    case CenterX:
            //        x -= width / 2;
            //        y -= fontMetrics.ascent;
            //        break;
            //    case CenterY:
            //        y -= fontMetrics.ascent / 2 + textSize * 0.15;
            //        break;
            //    case Center:
            //        x -= width / 2;
            //        y -= fontMetrics.ascent / 2 + textSize * 0.15;
            //        break;
            //    case Right:
            //        x -= width;
            //        y -= fontMetrics.ascent;
            //        break;
            //    case Right_CenterY:
            //        x -= width;
            //        y -= fontMetrics.ascent / 2 + textSize * 0.15;
            //        break;
            //}

            //if (bgColor != 0)
            //{
            //    paint.setColor(bgColor);
            //    canvas.drawRect(x - margin, y - textSize - margin,
            //            x + width + margin, y + margin, paint);
            //}

            //paint.setColor(color);
            //canvas.drawText(_text, x, y, paint);

            //return new Size(width, textSize);

            return new Size(100, 100);
        }

        /**
         * テキストを描画（複数行対応)
         *
         * @param canvas
         * @param text
         * @param alignment
         * @param textSize
         * @return
         */
        public static Size drawText(Graphics g, String text,
                                    UAlignment alignment, int textSize,
                                    float x, float y, Color color)
        {
            return drawText(g, text, alignment, textSize, x, y, color, 0);
        }

        public static Size drawText(Graphics g, String text,
                                    UAlignment alignment, int textSize,
                                    float x, float y, Color color, int bgColor)
        {
            if (text == null) return new Size(0, 0);

            //TextPaint textPaint = new TextPaint();

            //// アンチエイリアス
            //textPaint.setAntiAlias(true);

            //// x,yにラインを表示 for Debug
            //if (UDebug.drawTextBaseLine)
            //{
            //    drawLine(canvas, textPaint, x - 50, y, x + 50, y, 3, Color.YELLOW);
            //    drawLine(canvas, textPaint, x, y - 50, x, y + 50, 3, Color.YELLOW);
            //}


            //// 改行ができるようにTextPaintとStaticLayoutを使用する
            //textPaint.setTextSize(textSize);
            //textPaint.setColor(color);

            //StaticLayout mTextLayout = new StaticLayout(text, textPaint,
            //        canvas.getWidth() * 4 / 5, Layout.Alignment.ALIGN_NORMAL,
            //        1.0f, 0.0f, false);

            //Size size = getTextSize(mTextLayout);
            //switch (alignment)
            //{
            //    case CenterX:
            //        x = x - size.width / 2;
            //        break;
            //    case CenterY:
            //        y = y - size.height / 2;
            //        break;
            //    case Center:
            //        x = x - size.width / 2;
            //        y = y - size.height / 2;
            //        break;
            //    case None:
            //        break;
            //}

            //canvas.save();
            //canvas.translate(x, y);

            /////テキストの描画位置の指定
            //textPaint.setStyle(Paint.Style.FILL);
            //textPaint.setStrokeWidth(1);
            //textPaint.setColor(color);
            //mTextLayout.draw(canvas);
            //canvas.restore();

            //return size;

            return new Size(0, 0);
        }

        /**
         * テキストのサイズを取得する（マルチライン対応）
         * @param canvasW
         * @return
         */
        public static Size getTextSize(int canvasW, String text, int textSize)
        {
            //if (text == null) return new Size();

            //TextPaint textPaint = new TextPaint();
            //textPaint.setTextSize(textSize);
            //StaticLayout textLayout = new StaticLayout(text, textPaint,
            //        canvasW * 4 / 5, Layout.Alignment.ALIGN_NORMAL,
            //        1.0f, 0.0f, false);

            //int height = textLayout.getHeight();
            //int maxWidth = 0;
            //int _width;

            //// 各行の最大の幅を計算する
            //for (int i = 0; i < textLayout.getLineCount(); i++)
            //{
            //    _width = (int)textLayout.getLineWidth(i);
            //    if (_width > maxWidth)
            //    {
            //        maxWidth = _width;
            //    }
            //}

            //return new Size(maxWidth, height);
            return new Size();
        }

        //private static Size getTextSize(StaticLayout textLayout)
        //{
        //    int height = textLayout.getHeight();
        //    int maxWidth = 0;
        //    int _width;

        //    // 各行の最大の幅を計算する
        //    for (int i = 0; i < textLayout.getLineCount(); i++)
        //    {
        //        _width = (int)textLayout.getLineWidth(i);
        //        if (_width > maxWidth)
        //        {
        //            maxWidth = _width;
        //        }
        //    }

        //    return new Size(maxWidth, height);
        //}

        /**
         * １行テキストの描画サイズを取得する
         */
        public static Size getOneLineTextSize(Graphics g, String text, int textSize)
        {
            //paint.setTextSize(textSize);
            //return new Size((int)paint.measureText(text), textSize);
            return new Size();
        }

        /**
         * Bitmap画像
         */
        public static void drawBitmap(Graphics g, Bitmap image,
                                      float x, float y, int width, int height)
        {
            //canvas.drawBitmap(image, new Rect(0, 0, image.getWidth(), image.getHeight()),
            //        new Rect((int)x, (int)y, (int)x + width, (int)y + height), paint);

        }
        public static void drawBitmap(Graphics g, Bitmap image,
                                      Rectangle rect)
        {
            //paint.setColor(0xffffffff);
            //canvas.drawBitmap(image, new Rect(0, 0, image.getWidth(), image.getHeight()),
            //        rect, paint);
        }
    }
}
