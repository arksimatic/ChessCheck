using System;
using System.Collections.Generic;
using System.Text;
using ChessCheck.Pieces;

namespace ChessCheck
{
    abstract class Piece
    {

        public Position position;
        public Color color;
        public PieceType pieceType;

        public Piece(Position position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        public abstract override string ToString();

        public abstract string FullName();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Piece)) return false;
            if (((Piece)obj).position.Equals(position) &&((Piece)obj).color.Equals(color) && ((Piece)obj).pieceType.Equals(pieceType)) return true;
            return false;
        }

        public abstract Piece DeepCopy();
    }
}
