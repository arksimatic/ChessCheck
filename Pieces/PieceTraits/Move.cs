using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCheck.Pieces.PieceTraits
{
    class Move
    {
        public Piece piece;
        public Position endPosition;

        public Move(Piece piece, Position endPosition)
        {
            this.piece = piece;
            this.endPosition = endPosition;
        }

        public override string ToString()
        {
            return $"{piece.FullName()} from {piece.position.ToString()} moves to {endPosition}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Move)) return false;
            if (((Move)obj).piece.Equals(piece) && ((Move)obj).endPosition.Equals(endPosition)) return true;
            return false;
        }

    }
}
