using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Pawn : ChessPiece
    {
        private List<ChessCells> _attackingCells = new List<ChessCells>();

        public Pawn(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }
   

       public void ProduceAttackCells()
        {
          
            ChessCells leftAttackedCell;
            ChessCells rightAttackedCell;


            _attackingCells.Clear();

                 if (IsWhite && I - 1 > 0)
                    {

                       
                            if (J - 1 >= 0)
                            {
                                leftAttackedCell = ChessTable.GetChessCell(I - 1, J - 1);
                                _attackingCells.Add(leftAttackedCell);

                            }

                            if (J + 1 < 8)
                            {
                                rightAttackedCell = ChessTable.GetChessCell(I - 1, J + 1);

                                _attackingCells.Add(rightAttackedCell);
                            }
                     

                    }

                    if (!IsWhite && I + 1 < 7)

                    {
                               
                                            if (J - 1 >= 0)
                                            {
                                                leftAttackedCell = ChessTable.GetChessCell(I + 1, J - 1);
                                                _attackingCells.Add(leftAttackedCell);
                                            }
                                            if (J + 1 < 8)
                                            {
                                                rightAttackedCell = ChessTable.GetChessCell(I + 1, J + 1);

                                                _attackingCells.Add(rightAttackedCell);
                                            }


                    }


                    foreach (var cell in _attackingCells)
                    {
                        if (cell.HasPiece)
                        {
                            VallidCells.Add(cell);
                            AttactedEnemies.Add(cell.ChessPiece);

                        }

                    }

                        
      

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
            pawn.ProduceAttackCells();
        }

        public override void ProduceValidCells(ChessPiece pawn)
        {
            ComparerI comparer = new ComparerI();
            var currentPawn = (Pawn)pawn;
            currentPawn._validCells.Clear();
            int pawnDirection;

            if (!IsWhite)
            {
                pawnDirection = -1; // pawn goes down
            }
            else
            {
                pawnDirection = 1; // pawn goes up 
            }

            
                 currentPawn._validCells.Add(ChessTable.GetChessCell(currentPawn.I - pawnDirection*2, currentPawn.J));
                 currentPawn._validCells.Add(ChessTable.GetChessCell(currentPawn.I - pawnDirection, currentPawn.J));

            if (!currentPawn.IsWhite)
            {
                currentPawn.VallidCells.Sort(comparer);

            }
       
            if (!currentPawn._isFirstMove)
            {
        
                currentPawn.VallidCells.Remove(currentPawn.VallidCells[0]);
            }
        }

    }
}
