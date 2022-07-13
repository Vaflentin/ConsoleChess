using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chess
{
    static class ChessOutPut
    {
        public static void InitialMessage()
        {
            Console.Clear();
            ChessTableOutPut();

            Console.WriteLine();
            Console.Write("Make a piece [the format:(piece's name, x, y)] = ");

            var playersPiece = Console.ReadLine();
            var errorName = FormatIsValid(playersPiece);
 

            OutPutErrorMessages(errorName, playersPiece);

        }



        public static Errors FormatIsValid(string playersPiece)
        {
           

            try
            {
                if (Regex.IsMatch(playersPiece, "(?=.{3}$)[pbqhkrPBQHKR][a-hA-H][1-8]"))
                {
                    var i = Convert.ToInt32(playersPiece[2].ToString());
                    var j = (Collumns)Enum.Parse(typeof(Collumns), Convert.ToString(playersPiece[1]).ToLower(), ignoreCase: true);

                    if (ChessCells.GetCellCondition(i - 1, (int)j))
                    {
                        return Errors.ChessSquareAlreadyTaken;
                    }

                    else

                        return Errors.NoErrors;

                }

                else
                {

                    return Errors.InvalidFormat;

                }




            }
            catch (Exception)
            {
                return Errors.InvalidFormat;

                throw;
            }


        }


        public static void OutPutErrorMessages(Errors errorName, string playersPiece)
        {
            
            switch (errorName)
            {
                case Errors.NoErrors:
                    {
                        Player.CreatePiece(playersPiece);
                        InitialMessage();
                    }
                 


                    break;

                case Errors.InvalidFormat:

                    Console.WriteLine("Invalid format");
                    Console.WriteLine("Press any key");
                    Console.ReadKey();

                    InitialMessage();


                    break;

                case Errors.ChessSquareAlreadyTaken:

                    Console.WriteLine("The chess square is already taken by a piece");
                    Console.WriteLine("Press any key");
                    Console.ReadKey();

                    InitialMessage();

                    break;



                default:

                    Console.WriteLine("Unknown exception");

                    break;
            }
        }



        public static void ChessTableOutPut()
        {
            
            string[,] fullChessTable = ChessTable.MakeFakeChessTable();


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

       


        static void ChessLogOutPut()
        {
            List<string> chessLogs = ChessTable.ChessLogs;
            Console.WriteLine();
            foreach (string chessLog in chessLogs)
            {
               
                Console.Write($" {chessLog} ");
                
            }
            Console.WriteLine();
        }
        
    }
}
