using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace chess
{


    public enum Collumns : byte
    {
        a, b, c, d, e, f, g, h
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
            chessCell.SetCellCondition();
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


