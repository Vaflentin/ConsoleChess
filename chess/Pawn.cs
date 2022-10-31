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
 

        public List<ChessCells> AttackingCells
        {
            get { return _attackingCells; }
            set { _attackingCells = value; }
        }


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

        public override void CheckPiecesOnTheWay() // todo: Надо зарефакторить
        {
          

            List<ChessCells> tempolarValidCell = new List<ChessCells>(VallidCells);

            foreach (var cell in tempolarValidCell)
            {
                if (cell.HasPiece)
                {
                    VallidCells.Remove(cell);
                }
            }
           ProduceAttackCells();
        }

        public override void ProduceValidCells()
        {
            ComparerI comparer = new ComparerI();

           _validCells.Clear();
            int pawnDirection;

            if (!IsWhite)
            {
                pawnDirection = -1; // pawn goes down
            }
            else
            {
                pawnDirection = 1; // pawn goes up 
            }

            
                 _validCells.Add(ChessTable.GetChessCell(I - pawnDirection*2, J));
                 _validCells.Add(ChessTable.GetChessCell(I - pawnDirection, J));

            if (!IsWhite)
            {
                VallidCells.Sort(comparer);

            }
       
            if (!_isFirstMove)
            {
        
                VallidCells.Remove(VallidCells[0]);
            }
        }

    }
}
