using System;
using System.Collections.Generic;
using System.Text;
using ChessCheck.Pieces.PieceTraits;

namespace ChessCheck
{
    class Game
    {
        Board board = new Board().BasicSetup();
        Bot bot = new Bot();

        public void PlayWithHuman()
        {
            GameStatus gameStatus;
            while (true)
            {
                gameStatus = PlayerMove(Color.White);
                if (gameStatus != GameStatus.Active) break;

                gameStatus = PlayerMove(Color.Black);
                if (gameStatus != GameStatus.Active) break;
            }

            SummaryGame(gameStatus);
        }
        public void PlayWithBot()
        {
            Color playerColor = Color.White;
            Color botColor = Color.Black;

            GameStatus gameStatus;
            while (true)
            {
                gameStatus = PlayerMove(playerColor);
                if (gameStatus != GameStatus.Active) break;

                gameStatus = BotMove(botColor);
                if (gameStatus != GameStatus.Active) break;
            }

            SummaryGame(gameStatus);
        }

        public GameStatus PlayerMove(Color color)
        {
            Console.Clear();
            Console.WriteLine(board);

            GameStatus gameStatus;
            bool performed;
            do
            {
                gameStatus = board.Status(color);
                if (gameStatus != GameStatus.Active) break;
                Console.Write($"{color} move: ");
                string consoleMoveWhite = Console.ReadLine();
                performed = board.TryPerformMove(color, consoleMoveWhite);
            } while (!performed);

            return gameStatus;
        }
        public GameStatus BotMove(Color botColor)
        {
            Move botMove = bot.MakeMove(board, botColor);
            bool isPerformed = board.TryPerformMove(botColor, botMove);
            if (isPerformed == false) throw new Exception("Bot tried to make invalid move! ");

            return board.Status(botColor);
        }

        public void SummaryGame(GameStatus gameStatus)
        {
            if (gameStatus == GameStatus.Draw) Console.WriteLine("DRAW");
            if (gameStatus == GameStatus.BlackLose) Console.WriteLine("White won!");
            if (gameStatus == GameStatus.WhiteLose) Console.WriteLine("Black won!");
        }
    }
}
