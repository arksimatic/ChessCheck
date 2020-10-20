using System;
using System.Collections.Generic;
using System.Text;
using ChessCheck.Pieces;
using ChessCheck.Pieces.PieceTraits;

namespace ChessCheck
{
    class Board
    {
        private List<Piece> pieces;

        public Board()
        {
            pieces = new List<Piece>();
        }

        public Board BasicSetup()
        {
            //kings MUST BE created because otherwise WhiteKing and BlackKing will cause exception
            pieces.Add(new King(new Position(5, 4), Color.Black));
            pieces.Add(new King(new Position(1, 2), Color.White));

            pieces.Add(new Rook(new Position(2, 2), Color.Black));
            pieces.Add(new Rook(new Position(7, 7), Color.White));
            pieces.Add(new Rook(new Position(7, 5), Color.Black));

            #region pseudotesting
            //List<Position> positions = AllAttackedPositions(Color.White); //works ok
            //foreach (Position pos in positions)
            //Console.WriteLine(pos);

            //List<Position> position = NotOccupiedPositions(); //works ok
            //foreach (Position pos in position)
            //Console.WriteLine(pos);

            //List<Move> moves = AllPossibleMoves(Color.White); //works ok
            //foreach (Move move in moves)
            //    Console.WriteLine(move);

            //List<Move> legalMoves = AllLegalMoves(Color.White); //works ok
            //foreach (Move move in legalMoves)
            //    Console.WriteLine(move);
            #endregion

            return this;
        }

        public Board(List<Piece> pieces)
        {
            this.pieces = pieces;
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.pieceType == PieceType.King && piece.color == color)
                    return piece;
            }
            throw new Exception("One of the kings not found");
        }

        private bool IsCheck(Color color)
        {
            Piece king = King(color);
            List<Position> attackedPosition = AllAttackedPositions(ColorControl.OppositeColor(color));

            if (attackedPosition.Contains(king.position)) return true;
            else return false;
        }

        private List<Position> AllAttackedPositions(Color color)
        {
            List<Position> attackedPositions = new List<Position>();
            List<Position> notOccupiedPositions = NotOccupiedPositions();

            foreach (Piece piece in pieces)
            {
                if (piece.color == color)
                {
                    switch (piece.pieceType)  //change to strategy design pattern
                    {
                        case PieceType.King:
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    Position potentialPosition = new Position(piece.position.x + i, piece.position.y + j);
                                    if (potentialPosition.IsValid() && !potentialPosition.Equals(piece.position))
                                        attackedPositions.Add(potentialPosition);
                                }
                            }
                            break;

                        case PieceType.Rook:
                            for (int x = piece.position.x + 1; x <= 8; x++)
                            {
                                Position potentialPosition = new Position(x, piece.position.y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    attackedPositions.Add(potentialPosition);
                                else
                                {
                                    if (potentialPosition.IsValid()) //overall even if the position is occupied, we treat it as "attacked"
                                        attackedPositions.Add(potentialPosition); 
                                    break;
                                }
                            }
                            for (int x = piece.position.x - 1; x >= 1; x--)
                            {
                                Position potentialPosition = new Position(x, piece.position.y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    attackedPositions.Add(potentialPosition);
                                else
                                {
                                    if (potentialPosition.IsValid())
                                        attackedPositions.Add(potentialPosition);
                                    break;
                                }
                            }
                            for (int y = piece.position.y + 1; y <= 8; y++)
                            {
                                Position potentialPosition = new Position(piece.position.x, y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    attackedPositions.Add(potentialPosition);
                                else
                                {
                                    if (potentialPosition.IsValid())
                                        attackedPositions.Add(potentialPosition);
                                    break;
                                }
                            }
                            for (int y = piece.position.y - 1; y >= 1; y--)
                            {
                                Position potentialPosition = new Position(piece.position.x, y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    attackedPositions.Add(potentialPosition);
                                else
                                {
                                    if (potentialPosition.IsValid())
                                        attackedPositions.Add(potentialPosition);
                                    break;
                                }
                            }
                            break;
                    }
                }
            }
            return attackedPositions;
        }

        private List<Move> AllLegalMoves(Color color)
        {
            List<Move> allLegalMoves = new List<Move>();
            List<Move> allPossibleMoves = AllPossibleMoves(color);

            foreach(Move move in allPossibleMoves)
            {
                Board hipoteticalBoard = this.DeepCopy();
                if(hipoteticalBoard.TryPerformMove(color, move))
                {
                    if (!hipoteticalBoard.IsCheck(color))
                        allLegalMoves.Add(move);
                }
            }

            return allLegalMoves;
        }

        private List<Move> AllPossibleMoves(Color color)
        {
            List<Move> allPossibleMoves = new List<Move>();
            List<Position> notOccupiedPositions = NotOccupiedPositions();

            foreach (Piece piece in pieces)
            {
                if (piece.color == color)
                {
                    switch (piece.pieceType)  //change to strategy design pattern, again
                    {
                        case PieceType.King:
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    Position potentialPosition = new Position(piece.position.x + i, piece.position.y + j);
                                    if (potentialPosition.IsValid() && !potentialPosition.Equals(piece.position))
                                        allPossibleMoves.Add(new Move(piece, potentialPosition));
                                }
                            }
                            break;

                        case PieceType.Rook:
                            for (int x = piece.position.x + 1; x <= 8; x++)
                            {
                                Position potentialPosition = new Position(x, piece.position.y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                else
                                {
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                    break;
                                }
                            }
                            for (int x = piece.position.x - 1; x >= 1; x--)
                            {
                                Position potentialPosition = new Position(x, piece.position.y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                else
                                {
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                    break;
                                }
                            }
                            for (int y = piece.position.y + 1; y <= 8; y++)
                            {
                                Position potentialPosition = new Position(piece.position.x, y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                else
                                {
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                    break;
                                }
                            }
                            for (int y = piece.position.y - 1; y >= 1; y--)
                            {
                                Position potentialPosition = new Position(piece.position.x, y);
                                if (notOccupiedPositions.Contains(potentialPosition))
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                else
                                {
                                    allPossibleMoves.Add(new Move(piece, potentialPosition));
                                    break;
                                }
                            }
                            break;
                    }
                }
            }
            return allPossibleMoves;
        }

        public bool TryPerformMove(Color performingColor, Move move)
        {
            Piece? attackedPiece = ReturnPieceByPosition(move.endPosition);
            if (attackedPiece != null && attackedPiece.color == performingColor) return false; //WARING be careful whether the first statement is always executed before the second one, possible crash
            else pieces.Remove(attackedPiece);

            if (!move.endPosition.IsValid()) throw new Exception("in tryPerformMove the move is invalid. Are you missing input verification senpai?");
            int index = pieces.IndexOf(move.piece);
            pieces[index].position = move.endPosition;
            return true;
        }

        public void PrintAllLegalMoves(Color color)
        {
            List<Move> moves = AllLegalMoves(color);
            foreach (Move move in moves)
                Console.WriteLine(move);
        }

        public bool TryPerformMove(Color color, string consoleNotationMove)
        {
            if (consoleNotationMove == "help") PrintAllLegalMoves(color);
            if (consoleNotationMove == "clear" || consoleNotationMove == "cls")
            {
                Console.Clear();
                Console.WriteLine(this);
            }

            Move? move = ReturnMoveFromConsoleNotation(consoleNotationMove);
            if (move == null) return false;

            List<Move> legalMoves = AllLegalMoves(color);
            if (!legalMoves.Contains(move)) return false;

            if (!TryPerformMove(color, move)) return false; 

            return true;
        }

        public Move? ReturnMoveFromConsoleNotation(string consoleNotation)
        {
            char charStartX, charStartY, charEndX, charEndY;
            if (consoleNotation.Length == 5) //normal (mine) notation
            {
                charStartX = consoleNotation[0];
                charStartY = consoleNotation[1];
                charEndX = consoleNotation[3];
                charEndY = consoleNotation[4];
            }
            else if (consoleNotation.Length == 4) //shorter notation
            {
                charStartX = consoleNotation[0];
                charStartY = consoleNotation[1];
                charEndX = consoleNotation[2];
                charEndY = consoleNotation[3];
            }
            else return null;

            int? startX, startY, endX, endY;

            startX = ToProgramNotation.FromX(charStartX);
            startY = ToProgramNotation.FromY(charStartY);
            endX = ToProgramNotation.FromX(charEndX);
            endY = ToProgramNotation.FromY(charEndY);

            if (startX == null || startY == null || endX == null || endY == null) return null;

            Position startPosition = new Position((int)startX, (int)startY);
            Position endPosition = new Position((int)endX, (int)endY);
            Piece? piece = ReturnPieceByPosition(startPosition);
            if (piece != null) return new Move(piece, endPosition);

            return null;
        }

        private List<Position> NotOccupiedPositions() //small optimalization
        {
            List<Position> positions = new List<Position>();
            for(int i=1; i<=8; i++)
            {
                for(int j=1; j<=8; j++)
                {
                    positions.Add(new Position(i, j));
                }
            }

            foreach (Piece piece in pieces)
                positions.Remove(piece.position);

            return positions;
        }

        public Piece? ReturnPieceByPosition(Position position)
        {
            foreach(Piece piece in pieces)
                if (piece.position.Equals(position)) return piece;
            return null; 
        }

        public GameStatus Status(Color performingColor)
        {
            List<Move> moves = AllLegalMoves(performingColor);
            if (moves.Count > 0) return GameStatus.Active;
            if (IsCheck(performingColor))
            {
                if (performingColor == Color.Black) return GameStatus.BlackLose;
                if (performingColor == Color.White) return GameStatus.WhiteLose;
            }
            return GameStatus.Draw;
        }

        public override string ToString()
        {
            StringBuilder boardString = new StringBuilder();
            boardString.Append("  ABCDEFGH\n");
            for(int y=1; y<=8; y++)
            {
                boardString.Append($"{(8-y)%8+1}|");
                for(int x=1; x<=8; x++)
                {
                    Piece? piece = ReturnPieceByPosition(new Position(x, y));
                    if (piece == null) boardString.Append(" ");
                    else boardString.Append(piece.ToString());
                }
                boardString.Append("|\n");
            }

            return boardString.ToString();
        }

        public Board DeepCopy()
        {
            List<Piece> newPieces = new List<Piece>();
            foreach (Piece piece in pieces)
                newPieces.Add(piece.DeepCopy());
            return new Board(newPieces);
        }
    }
}
