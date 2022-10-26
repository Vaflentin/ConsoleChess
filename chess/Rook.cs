using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Rook : Queen
    {

        public Rook(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void ProduceValidCells(ChessPiece rook)
        {
            rook.VallidCells.Clear();
            Rook currentRook = (Rook)rook;

            currentRook.ProduceStraightCells();
            //currentRook.CheckPiecesOnTheWay(currentRook);
          
            MergeVallidCellsArray();
            DeleteInvalidCellsFromList(currentRook);
        }

        //public override Errors ValidateSquares(ChessPiece rook, int i, int j)
        //{



        //    if (!ProcessValidCells(i, j, rook.VallidCells))
        //    {
        //        return Errors.InvalidSquare;
        //    }


        //    return Errors.NoErrors;
        //}
    }
}
