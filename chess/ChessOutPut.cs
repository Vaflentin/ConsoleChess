using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace chess
{
    public enum Errors
    {
        NoErrors,
        InvalidFormat,
        ChessSquareAlreadyTaken,
        UnknownExcpetion,
        InvalidSquare, 
        CellDoesNotContainSuchPiece,
        WrongCommand,
        WrongKey

    }
   


    public static class ChessOutPut 
    {
       
        private static string[,] fakeChessTable = new string[10, 10];
        private static string[,] fullChessTable;
  


        public static string[,] GetFakeChessTable()
        {
            var currentFakeChessTable = fakeChessTable;
            return currentFakeChessTable;
        }

  
        public static void ChessTableOutPut()
        {
            Console.Clear();
            
            fullChessTable = CreateFakeChessTable();


            for (int i = 0; i < fullChessTable.GetLength(0); i++)
            {
                for (int j = 0; j < fullChessTable.GetLength(1); j++)
                {
                    Console.Write(fullChessTable[i, j]);

                }
                Console.WriteLine();
            }
            ChessLogOutPut();
        }


        private static void ChessLogOutPut()
        {
            List<string> chessLogs = ChessTable.ChessLogs;
            Console.WriteLine();
            foreach (string chessLog in chessLogs)
            {
               
                Console.Write($" {chessLog} ");
                
            }
            Console.WriteLine();
        }
        public static void DisableHighLighting()
        {
            var chessTable = ChessTable.ChessCells;

            foreach (var cell in chessTable)
            {
                cell.IsHighLighted = false;
            }
        }
        public static void HighLightPossibleMoves(ChessPiece piece)
        {
     


            foreach (var vallidCell in piece.VallidCells)
            {


                vallidCell.IsHighLighted = true;

                if (vallidCell.CellIsWhite)
                {
                    vallidCell.SetColorOfCell( "▓▓▓");
                }
                else
                    vallidCell.SetColorOfCell( "▒▒▒");  
               
            }

        }
        private static string[,] CreateFakeChessTable()
        {
            var row = 9;

            for (var i = 0; i < fakeChessTable.GetLength(0); i++)
            {

                for (var j = 0; j < fakeChessTable.GetLength(1); j++)
                {
                    if (((i < 1) || ((i > 8))) && ((j >= 1) && (j <= 8)))
                    {

                        fakeChessTable[i, j] = ($"  {Convert.ToString((Collumns)(j - 1))}");

                    }

                    if (((i < 9) && (i > 0)) && ((j == 0) || (j == 9)))
                    {

                        fakeChessTable[i, j] = Convert.ToString($"{row}");

                    }


                    else
                    {
                        if (((i >= 1) && (j >= 1)) && (i <= 8) && (j <= 8))
                        {
                            var currentCell = ChessTable.GetChessCell(i - 1, j - 1);
                            if (((1 - i + j) % 2 == 0) && currentCell.IsHighLighted != true)
                            {

                                currentCell.SetColorOfCell("   ");

                            }
                            else
                                if (currentCell.IsHighLighted != true)
                            {
                                currentCell.SetColorOfCell("███");
                            }
                               

                            if (currentCell.HasPiece)
                            {
                                currentCell.SetCellCondition();
                            }

                            fakeChessTable[i, j] = currentCell.CellsCondition;

                        }

                    }

                }

                row--;
            }
            return fakeChessTable;
        }



    }

}
