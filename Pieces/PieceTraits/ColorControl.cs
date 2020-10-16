using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCheck.Pieces.PieceTraits
{
    public static class ColorControl
    {
        public static Color OppositeColor(Color color)
        {
            if (color == Color.Black) return Color.White;
            return Color.Black;
        }
    }
}
