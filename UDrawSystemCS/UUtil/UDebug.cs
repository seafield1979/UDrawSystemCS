using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDrawSystemCS.UUtil
{
    /**
     * Created by shutaro on 2016/11/07.
     */

    public class UDebug
    {
        // Debug mode
        public const bool isDebug = false;

        // IconをまとめたブロックのRECTを描画するかどうか
        public const bool DRAW_ICON_BLOCK_RECT = false;

        public const bool drawIconId = false;

        // UDrawableオブジェクトの描画範囲をライン描画
        public const bool drawRectLine = false;

        // Select時にログを出力
        public const bool debugDAO = false;

        // テキストのベース座標に+を描画
        public const bool drawTextBaseLine = false;
        
    }

}
