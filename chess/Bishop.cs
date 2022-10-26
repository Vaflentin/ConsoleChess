using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Bishop : Queen
    {


        public Bishop(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {


        }



        //public override Errors ValidateSquares(ChessPiece bishop, int i, int j)
        //{

        //    if (ProcessValidCells(i, j, bishop.VallidCells) != true)
        //    {
        //        return Errors.InvalidSquare;
        //    }

        //    return Errors.NoErrors;

        //}

        public override void ProduceValidCells(ChessPiece bishop)
        {
            bishop.VallidCells.Clear();
            Bishop currentBishop = (Bishop)bishop;
            currentBishop.ProduceDiagonalCells();
            currentBishop.CheckPiecesOnTheWay();
            //CheckAlliesOnDiagonalyLines((currentBishop));
            MergeVallidCellsArray();
            DeleteInvalidCellsFromList(bishop);
        }
    }

}
