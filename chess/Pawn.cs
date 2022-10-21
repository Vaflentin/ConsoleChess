using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Pawn : ChessPiece
    {

        private bool isFirstMove = true;




        public Pawn(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }
        public override Errors ValidateSquares(ChessPiece pawn, int i, int j)
        {
            Pawn currentPawn = (Pawn)pawn;




            if (!ProcessValidCells(i, j, pawn.VallidCells))
            {
                return Errors.InvalidSquare;
            }

            currentPawn.isFirstMove = false;


            return Errors.NoErrors;
        }

        public override void CheckPiecesOnTheWay(ChessPiece chessPiece)
        {
            Pawn pawn = (Pawn)chessPiece;
            ComparerI comparer = new ComparerI();
            List<ChessCells> tempolarValidCell = new List<ChessCells>(pawn.VallidCells);

            int counter = 0;
            if (pawn.IsWhite)
            {



                //tempolarValidCell.Sort(comparer);
                //pawn.VallidCells.Sort(comparer);

                foreach (var cell in tempolarValidCell)
                {




                    if (cell.HasPiece && cell.ChessPiece.IsWhite)
                    {
                        pawn.validCells.RemoveRange(counter, pawn.validCells.Count - counter);
                        if (pawn.validCells.Count == 0)
                        {
                            break;
                        }

                    }

                    counter++;
                }
            }
            if (!pawn.IsWhite)
            {

                tempolarValidCell.Sort((x, y) => x.I.CompareTo(y.I));
                pawn.validCells.Sort((x, y) => x.I.CompareTo(y.I));

                foreach (var cell in tempolarValidCell)
                {


                    if (cell.HasPiece && !cell.ChessPiece.IsWhite)
                    {
                        pawn.validCells.RemoveRange(counter, pawn.validCells.Count - counter);
                    }

                    counter++;
                }

            }
        }

        public override void ProduceValidCells(ChessPiece pawn)
        {
            var currentPawn = (Pawn)pawn;
            currentPawn.validCells.Clear();


            if (pawn.IsWhite)
            {
                if (!currentPawn.isFirstMove)
                {
                    //currentPawn.validCells.Add((currentPawn.I - 1) + currentPawn.J.);
                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - 1, currentPawn.J));
                }
                else
                {
                    //currentPawn.validCells.Add((currentPawn.I - 2).ToString() + currentPawn.J.ToString());
                    //currentPawn.validCells.Add((currentPawn.I - 1).ToString() + currentPawn.J.ToString());

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - 2, currentPawn.J));
                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - 1, currentPawn.J));
                }
            }
            else
            {
                if (!currentPawn.isFirstMove)
                {
                    //currentPawn.validCells.Add((currentPawn.I + 1).ToString() + currentPawn.J.ToString());

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I + 1, currentPawn.J));
                }
                else
                {
                    //currentPawn.validCells.Add((currentPawn.I + 2).ToString() + currentPawn.J.ToString());
                    //currentPawn.validCells.Add((currentPawn.I + 1).ToString() + currentPawn.J.ToString());

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I + 2, currentPawn.J));

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I + 1, currentPawn.J));

                }
            }

            CheckPiecesOnTheWay(currentPawn);


        }



    }
}
