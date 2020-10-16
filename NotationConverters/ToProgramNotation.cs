using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCheck
{
    static class ToProgramNotation
    {
        public static int? FromX(char consoleX)
        {
            switch (consoleX)
            {
                case 'a': case 'A': return 1;
                case 'b': case 'B': return 2;
                case 'c': case 'C': return 3;
                case 'd': case 'D': return 4;
                case 'e': case 'E': return 5;
                case 'f': case 'F': return 6;
                case 'g': case 'G': return 7;
                case 'h': case 'H': return 8;
                default: return null;
            }
        }

        public static int? FromY(char consoleY)
        {
            switch (consoleY) //this implementation looks awful but I decided that it's more clear what is happening here
            {
                case '1': return 8;
                case '2': return 7;
                case '3': return 6;
                case '4': return 5;
                case '5': return 4;
                case '6': return 3;
                case '7': return 2;
                case '8': return 1;
                default: return null;
            }
        }
    }
}


