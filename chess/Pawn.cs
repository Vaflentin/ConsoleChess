using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Pawn : ChessPiece
    {


        public Pawn(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }
   

        public override void CheckPiecesOnTheWay(ChessPiece chessPiece) // todo: Надо зарефакторить
        {
            Pawn pawn = (Pawn)chessPiece;

            List<ChessCells> tempolarValidCell = new List<ChessCells>(pawn.VallidCells);

            foreach (var cell in tempolarValidCell)
            {
                if (cell.HasPiece)
                {
                    pawn.VallidCells.Remove(cell);
                }
            }
           
        }

        public override void ProduceValidCells(ChessPiece pawn)
        {
            var currentPawn = (Pawn)pawn;
            currentPawn.validCells.Clear();
            int pawnDirection;

            if (!IsWhite)
            {
                pawnDirection = -1; // pawn goes down
            }
            else
            {
                pawnDirection = 1; // pawn goes up 
            }

                 currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - pawnDirection*2, currentPawn.J));
                 currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - pawnDirection, currentPawn.J));
            
            if (!currentPawn._isFirstMove)
            {
                currentPawn.VallidCells.Remove(currentPawn.VallidCells[0]);
            }
        }

    }
}
