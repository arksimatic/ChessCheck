using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCheck
{
    class ToConsoleNotation
    {
        public static string FromX(int programX)
        {
            switch (programX)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                case 6: return "F";
                case 7: return "G";
                case 8: return "H";
                default: return null;
            }
        }

        public static string FromY(int programY)
        {
            switch (programY)
            {
                case 1: return "8";
                case 2: return "7";
                case 3: return "6";
                case 4: return "5";
                case 5: return "4";
                case 6: return "3";
                case 7: return "2";
                case 8: return "1";
                default: return null;
            }
        }
    }
}
