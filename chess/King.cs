using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class King : Queen
    {
        List<ChessCells> SuspiciousChecksCells { get; set; }
        private bool _isChecked;
        public King(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }
        private void CheckIsKingUnderAttack()
        {
            ProduceDiagonalCells();
            ProduceStraightCells();

            MergeVallidCellsArray(
                SuspiciousChecksCells,
                diagonalLowerLeftValidCells, 
                diagonalLowerRightValidCells, 
                diagonalUpperLeftValidCells,
                diagonalUpperRightValidCells,
                straightDownLineValidCells,  
                straightLeftLineValidCells, 
                straightRightLineValidCells, 
                straightUpLineValidCells
                );

            SortThroughCellLists();
        }

        protected override void SortThroughCellLists()
        {
            var allListOfValidCells = GetAllListFields();

            foreach (var list in allListOfValidCells)
            {
                ProccessSuspiciousCells(list);
            }
        }

        private void ProccessSuspiciousCells(List<ChessCells> currentList)
        {
            ComparerI comparerI = new ComparerI();

            if (currentList == straightUpLineValidCells || currentList == straightLeftLineValidCells
                || currentList == diagonalUpperLeftValidCells || currentList == diagonalUpperRightValidCells)
            {
                currentList.Sort(comparerI);
            }

       
        }

        public override void CheckPiecesOnTheWay() // todo:: рефакторить
        {
            List<ChessCells> tempolarCells = new List<ChessCells>(VallidCells);

            foreach (var cell in tempolarCells)
            {

                if (cell.HasPiece)
                {
                    if (IsWhite && cell.ChessPiece.IsWhite)
                    {
                        ProtectedAllies.Add(cell.ChessPiece);
                        VallidCells.Remove(cell);
                    }
                    else AttactedEnemies.Add(cell.ChessPiece);

                    if (!IsWhite && !cell.ChessPiece.IsWhite)
                    {
                        ProtectedAllies.Add(cell.ChessPiece);
                        VallidCells.Remove(cell);
                    }
                    else AttactedEnemies.Add(cell.ChessPiece);
                }
            }

        }

        public override void ProduceValidCells()
        {
  
            _validCells.Clear();

            for (int i = I - 1; i <= I + 1; i++)
            {
                for (int j = J - 1; j <= J + 1; j++)
                {
                    if ((i < 8 && i >= 0) && (j < 8 && j >= 0))
                    {
           
                        _validCells.Add(ChessTable.GetChessCell(i, j));
                    }


                }
            }
            CheckIsKingUnderAttack();

        }

    }
}
