using System;
using System.Collections.Generic;
using System.Text;
using ChessCheck.Pieces;

namespace ChessCheck
{
    class Rook : Piece
    {
        public Rook(Position position, Color color) : base(position, color)
        {
            this.pieceType = PieceType.Rook;
        }

        public override string ToString()
        {
            if (color == Color.Black) return "r";
            else return "R";
        }

        public override string FullName()
        {
            if (color == Color.Black) return "Black Rook";
            else return "White Rook";
        }

        public override Piece DeepCopy()
        {
            return new Rook(position, color);
        }
    }
}
