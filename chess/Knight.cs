using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override void CheckPiecesOnTheWay(ChessPiece chessPiece) // todo: зарефакторить с королем
        {
            List<ChessCells> tempolarList = new List<ChessCells>(chessPiece.VallidCells);

            foreach (var cell in tempolarList)
            {
                if (chessPiece.IsWhite)
                {
                    if (cell.HasPiece && cell.ChessPiece.IsWhite)
                    {
                        chessPiece.VallidCells.Remove(cell);
                    }
                }
                if (!chessPiece.IsWhite)
                {
                    if (cell.HasPiece && !cell.ChessPiece.IsWhite)
                    {
                        chessPiece.VallidCells.Remove(cell);
                    }
                }

            }
        }
        private static void CheckKnightMovesValidation(Knight knight, KnightMoves knightMoves)
        {
            switch (knightMoves)
            {
                case KnightMoves.UpperLeftTopCell:

                    if ((knight.I - 2 >= 0 && knight.J - 1 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 2, knight.J - 1));
                    }

                    break;

                case KnightMoves.UpperLeftBottomCell:
                    if ((knight.I - 1 >= 0 && knight.J - 2 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J - 2));
                    }
                    break;

                case KnightMoves.LowerLeftBottomCell:
                    if ((knight.I + 1 <= 7 && knight.J - 2 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 1, knight.J - 2));
                    }
                    break;
                case KnightMoves.LowerLeftTopCell:
                    if ((knight.I + 2 <= 7 && knight.J - 1 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 2, knight.J - 1));
                    }

                    break;
                case KnightMoves.UpperRightTopCell:
                    if ((knight.I - 2 > +0 && knight.J + 1 <= 7))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 2, knight.J + 1));
                    }
                    break;
                case KnightMoves.UpperRightBottomCell:
                    if ((knight.I - 1 >= 0 && knight.J + 2 <= 7))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J + 2));
                    }
                    break;
                case KnightMoves.LowerRightTopCell:
                    if ((knight.I + 2 <= 7 && knight.J + 1 <= 7))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 2, knight.J + 1));
                    }

                    break;
                case KnightMoves.LowerRightBottomCell:
                    if ((knight.I + 1 <= 7 && knight.J + 2 <= 7))
                    {

                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 1, knight.J + 2));

                    }
                    break;
                default:
                    break;
            }
        }
        public override void ProduceValidCells(ChessPiece knight)
        {
            knight.VallidCells.Clear();


            for (int i = 0; i <= 8; i++)
            {
                CheckKnightMovesValidation((Knight)knight, (KnightMoves)i);
            }
            //CheckPiecesOnTheWay(knight);



            DeleteInvalidCellsFromList(knight);
        }


    }
}
