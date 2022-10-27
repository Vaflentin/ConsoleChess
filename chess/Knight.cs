using System.Collections.Generic;

namespace chess
{
    class Knight : ChessPiece
    {
        enum KnightMoves
        {
            UpperLeftTopCell,
            UpperLeftBottomCell,
            UpperRightTopCell,
            UpperRightBottomCell,
            LowerLeftTopCell,
            LowerLeftBottomCell,
            LowerRightTopCell,
            LowerRightBottomCell
        }
        public Knight(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void CheckPiecesOnTheWay() // todo: зарефакторить с королем
        {
            List<ChessCells> tempolarList = new List<ChessCells>(VallidCells);

            foreach (var cell in tempolarList)
            {
               
                    if (cell.HasPiece)
                    {
                        if (IsWhite)
                        {
                            if (cell.ChessPiece.IsWhite)
                            {
                               ProtectedAllies.Add(cell.ChessPiece);
                               VallidCells.Remove(cell);
                            }
                            else
                                AttactedEnemies.Add(cell.ChessPiece);

                         }

                        if (!IsWhite)
                        {
                            if (!cell.ChessPiece.IsWhite)
                            {
                               ProtectedAllies.Add(cell.ChessPiece);
                               VallidCells.Remove(cell);
                            }
                            else
                                AttactedEnemies.Add(cell.ChessPiece);

                        }   
                    }

            }
        }
        private void CheckKnightMovesValidation( KnightMoves knightMoves)
        {
            switch (knightMoves)
            {
                case KnightMoves.UpperLeftTopCell:

                    if ((I - 2 >= 0 && J - 1 >= 0))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I - 2, J - 1));
                    }

                    break;

                case KnightMoves.UpperLeftBottomCell:
                    if ((I - 1 >= 0 && J - 2 >= 0))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I - 1, J - 2));
                    }
                    break;

                case KnightMoves.LowerLeftBottomCell:
                    if ((I + 1 <= 7 && J - 2 >= 0))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I + 1, J - 2));
                    }
                    break;
                case KnightMoves.LowerLeftTopCell:
                    if ((I + 2 <= 7 && J - 1 >= 0))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I + 2, J - 1));
                    }

                    break;
                case KnightMoves.UpperRightTopCell:
                    if ((I - 2 > +0 && J + 1 <= 7))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I - 2, J + 1));
                    }
                    break;
                case KnightMoves.UpperRightBottomCell:
                    if ((I - 1 >= 0 && J + 2 <= 7))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I - 1, J + 2));
                    }
                    break;
                case KnightMoves.LowerRightTopCell:
                    if ((I + 2 <= 7 && J + 1 <= 7))
                    {
                        VallidCells.Add(ChessTable.GetChessCell(I + 2, J + 1));
                    }

                    break;
                case KnightMoves.LowerRightBottomCell:
                    if ((I + 1 <= 7 && J + 2 <= 7))
                    {

                        VallidCells.Add(ChessTable.GetChessCell(I + 1, J + 2));

                    }
                    break;
                default:
                    break;
            }
        }
        public override void ProduceValidCells()
        {
            VallidCells.Clear();


            for (int i = 0; i <= 8; i++)
            {
                CheckKnightMovesValidation((KnightMoves)i);
            }
            //CheckPiecesOnTheWay(knight);


        }


    }
}
