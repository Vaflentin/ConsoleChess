using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chess
{
    public class ChessCells
    {
        private int _i;
        private int _j;

        private string _cellsCondition;
        private bool _hasPiece;
        private ChessPiece _chessPiece;
        private bool _cellIsWhite;
        private bool _isHighLighted;



        public int I
        {
            get { return _i; }
            set { _i = value; }
        }
        public int J
        {
            get { return _j; }
            set { _j = value; }
        }



        public bool IsHighLighted
        {
            get { return _isHighLighted; }
            set { _isHighLighted = value; }
        }





        public ChessCells(int i, int j)
        {
            I = i;
            J = j;

            if ((1 - i + j) % 2 == 0)
            {

                CellIsWhite = false;

            }
            else
            {

                CellIsWhite = true;

            }

        }

        public void SetHasPiece(bool hasPiece)
        {
            HasPiece = hasPiece;
        }

        public void SetChessPiece(ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;
        }

        public ChessPiece ChessPiece
        {
            get
            {
                return _chessPiece;
            }

            set
            {
                _chessPiece = value;
            }
        }


        public string CellsCondition
        {
            get
            {
                return _cellsCondition;
            }

            private set
            {
                _cellsCondition = value;
            }
        }

        public bool CellIsWhite
        {
            get { return _cellIsWhite; }
            private set { _cellIsWhite = value; }
        }

        public string ColorOfCell
        {
            get
            {
                return _cellsCondition;
            }
            private set
            {
                _cellsCondition = value;
            }
        }

        public void SetColorOfCell(string cellColor)
        {
            _cellsCondition = cellColor;
        }

        public bool HasPiece
        {
            get
            {
                return _hasPiece;
            }

            private set
            {
                _hasPiece = value;
            }
        }



        public static bool GetCellCondition(int i, int j)
        {
            var currentCell = ChessTable.GetChessCell(i, j);
            return currentCell.HasPiece;
        }


        public void SetCellCondition()
        {

            if (HasPiece == false)
            {
                CellsCondition = CellsCondition.Remove(1, 1).Insert(1, CellsCondition[0].ToString());

            }
            else
            {

                CellsCondition = CellsCondition.Remove(1, 1).Insert(1, ChessPiece.PieceName);

            }

        }

    }

}
