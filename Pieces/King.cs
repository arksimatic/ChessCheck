using System;
using System.Collections.Generic;
using System.Text;
using ChessCheck.Pieces;

namespace ChessCheck
{
    class King : Piece
    {
        public King(Position position, Color color) : base(position, color)
        {
            this.pieceType = PieceType.King;
        }

        public override string ToString()
        {
            if (color == Color.Black) return "k";
            else return "K";
        }

        public override string FullName()
        {
            if (color == Color.Black) return "Black King";
            else return "White King";
        }

        public override Piece DeepCopy()
        {
            return new King(position, color);
        }
    }
}
