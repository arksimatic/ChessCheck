using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCheck.Pieces
{
    class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsValid()
        {
            if (x >= 1 && x <= 8 && y >= 1 && y <= 8) return true;
            else return false;
        }

        public override string ToString()
        {
            return $"{ToConsoleNotation.FromX(x)}{ToConsoleNotation.FromY(y)}"; //change to A, B, C, D etc.
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Position)) return false;
            if (this.x == ((Position)obj).x && this.y == ((Position)obj).y) return true;
            return false;
        }
    }
}
