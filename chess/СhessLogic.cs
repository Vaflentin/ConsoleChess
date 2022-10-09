using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace chess
{

    public enum PieceNames : byte
    {
        p,
        b,
        q,
        k,
        h,
        r
    }
    public enum Collumns : byte
    {
        a, b, c, d, e, f, g, h
    }

    class СhessLogic
    {



        static void Main()
        {
            
            Player firstPlayer = new Player(true);
            Player secondPlayer = new Player(false);
            ChessTable chessTable = ChessTable.Initialize();

            ChessMenu.CreateMenuFrame();


        }
    }




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

        public void SetColorOfCell(ChessCells chessCell, string cellColor)
        {
            chessCell._cellsCondition = cellColor;
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
      

        public void SetCellCondition(ChessCells chessCell)
        {

            if (chessCell.HasPiece == false)
            {
                chessCell.CellsCondition = chessCell.CellsCondition.Remove(1, 1).Insert(1, chessCell.CellsCondition[0].ToString());
             
            }
            else
            {

                chessCell.CellsCondition = chessCell.CellsCondition.Remove(1, 1).Insert(1, chessCell.ChessPiece.PieceName);

            }

        }

    }

  
    class ChessTable 
    {
        private static ChessTable single = null;
        private static ChessCells[,] chessCells = new ChessCells[8, 8];
        private static List<string> chessLogs = new();


        public static ChessCells[,] ChessCells
        {
            get { return chessCells; }

        }



        public static List<string> ChessLogs
        {
            get { return chessLogs; }
            private set { chessLogs = value; }
        }


        public static void SetChessLog(string pieceName, int i, int j)
        {
            chessLogs.Add($"{pieceName}{(Collumns)j}{Convert.ToString(8 - i)}");
        }

        public static void PlacePiece(ChessPiece chessPiece)
        {

            chessCells[chessPiece.I, chessPiece.J].SetHasPiece(true);
            chessCells[chessPiece.I, chessPiece.J].SetChessPiece(chessPiece);

           

        }

        public static void DeletePiece(int i, int j)
        {
         
            var chessCell = GetChessCell(i, j);

            chessCell.SetHasPiece(false);

            chessCell.SetCellCondition(chessCell);

            chessCell.ChessPiece = null;

        }

        public static void ClearChessBoard()
        {
            for (int i = 0; i < chessCells.GetLength(0); i++)
            {
                for (int j = 0; j < chessCells.GetLength(0); j++)
                {
                    DeletePiece(i, j);
                }
            }
        }

        public static ChessCells GetChessCell(int i, int j)
        {
            return chessCells[i, j];
        }


        protected ChessTable()
        {

        }


        public static ChessTable Initialize()
        {
            if (single == null)
            {

                single = new ChessTable();
                Console.OutputEncoding = System.Text.Encoding.UTF8;


                for (int i = 0; i < chessCells.GetLength(0); i++)
                {
                    for (int j = 0; j < chessCells.GetLength(1); j++)
                    {

                        chessCells[i, j] = new ChessCells(i, j);

                    }

                }

            }

            return single;
        }
    }
}


