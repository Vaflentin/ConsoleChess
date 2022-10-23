using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public static class ChessMessages
    {
        private const string INVALID_FORMAT_ERROR = "Invalid format";
        private const string SQUARE_ALREADY_TAKEN_ERROR = "The square already taken by a piece";
        private const string UNKNOWN_EXCEPTION = "UKNOWN EXCEPTION";
        private const string INVALID_SQUARE = "Invalid Square";
        private const string CELL_DOES_NOT_CONTAIN_SUCH_PIECE = "The selected cell doesn't contains such a piece";
        private const string WRONG_KEY_WAS_PRESSED = "The wrong key was pressed";
        private const string NO_ANY_LEGAL_MOVES = "There is not a single legal move";

        public static void OutPutWrongCommandMessage()
        {
            Console.WriteLine("Wrong command");
            Console.Write("Press any key to try again");

            Console.ReadKey();
            ChessCommand.InitializeUsersCommands();
        }

      
        public static void OutPutDeletionMessage()
        {
            Console.Clear();
            ChessOutPut.ChessTableOutPut();

            Console.WriteLine();
            Console.Write("Enter the piece you want to delete: ");

            var playersPiece = Console.ReadLine();


            if (ChessDataValidation.CheckFormatFalidation(playersPiece) == Errors.NoErrors || ChessDataValidation.CheckFormatFalidation(playersPiece) == Errors.ChessSquareAlreadyTaken)
            {
                playersPiece = ChessDataValidation.ConvertCoordinates(playersPiece);
                var i = Convert.ToInt32(playersPiece[0].ToString());
                var j = Convert.ToInt32(playersPiece[1].ToString());

                ChessTable.DeletePiece(i, j);
            }
            else {

                OutPutErrorMessages(Errors.InvalidFormat);
                OutPutDeletionMessage();

                 }



        }

        public static void OutPutMakingPieceMessage()
        {
            string convertedPlayerPiece;
            int j;
            int i;
            PieceNames pieceName;


            ChessOutPut.ChessTableOutPut();
      
            Console.WriteLine();
            Console.Write("Make a piece [the format:(piece's name, x, y)] = ");

            var playersPiece = Console.ReadLine();

            if (playersPiece == "back")
            {
                ChessCommand.ProcessFreeModeCommands();
            }

            var errorName = ChessDataValidation.CheckFormatFalidation(playersPiece);

            if (errorName != Errors.NoErrors)
            {
                OutPutErrorMessages(errorName);
            }
            else
            {
                convertedPlayerPiece = ChessDataValidation.ConvertCoordinates(playersPiece);
                i = Convert.ToInt32(convertedPlayerPiece[0].ToString());
                j = Convert.ToInt32(convertedPlayerPiece[1].ToString());
                pieceName = (PieceNames)Enum.Parse(typeof(PieceNames), convertedPlayerPiece[2].ToString().ToLower(), ignoreCase: true);

                Player.CreatePiece(i,j,pieceName);
                OutPutMakingPieceMessage();
            }

  

      
        }





        public static void OutPutErrorMessages(Errors errorName)
        {

            switch (errorName)
            {

                case Errors.NoErrors:
                               break;

                case Errors.InvalidFormat:

                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(INVALID_FORMAT_ERROR);
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();

                        Console.ResetColor();

                        ChessCommand.ProcessBackCommand();

                    }


                    break;

                case Errors.ChessSquareAlreadyTaken:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(SQUARE_ALREADY_TAKEN_ERROR);
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();

                        Console.ResetColor();
                        ChessCommand.ProcessBackCommand();

                    }

                    break;

                case Errors.InvalidSquare:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(INVALID_SQUARE);
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();

                        Console.ResetColor();
                               ChessCommand.ProcessBackCommand();
                    }


                    break;

                case Errors.CellDoesNotContainSuchPiece:
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(CELL_DOES_NOT_CONTAIN_SUCH_PIECE);
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();

                        Console.ResetColor();
                        ChessCommand.ProcessBackCommand();
                    }
                    break;

                case Errors.WrongKey:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(WRONG_KEY_WAS_PRESSED);
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();

                        Console.ResetColor();
                        ChessCommand.ProcessBackCommand();
                    }
                    break;

                //case Errors.NoAnyLegalMove:
                //    {
                //        Console.ForegroundColor = ConsoleColor.Yellow;
                //        Console.WriteLine(NO_ANY_LEGAL_MOVES);
                //        Console.WriteLine("Press any key to try again");
                //        Console.ReadKey();

                //        Console.ResetColor();

                //        ChessCommand.InitializeUsersCommands();
                //    }
                //    break;

                default:

                    Console.WriteLine(UNKNOWN_EXCEPTION);
                    ChessCommand.ProcessBackCommand();

                    break;
            }

            
        }
     
    }
}


