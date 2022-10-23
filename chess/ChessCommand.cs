using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chess
{
    enum UserCommands
    {
        ng,
        clear,
        fm,
        play,
        back,
        move,
        create,
        delete,
        start,
        place
          
    }
    abstract class ChessCommand
    {
        static string userCommand;
        static UserCommands currentCommand;
        static UserCommands previousCommand;
        public static void ProcessUsersCommand(UserCommands currentCommand)
        {
            ChessOutPut.ChessTableOutPut();
            switch (currentCommand)
            {

                case UserCommands.ng:
                    {
                        Player.SetFullChessBoard();
                        InitializeUsersCommands();

                    }
                    break;

                case UserCommands.clear:
                    {
                        ChessTable.ClearChessBoard();
                        InitializeUsersCommands();
                    }
                    break;

                case UserCommands.fm:
                    {
                        ProcessFreeModeCommands();
                    }
                    break;

                case UserCommands.play:
                    {


                    }
                    break;



                default:

                    ChessMessages.OutPutWrongCommandMessage();


                    break;
            }
            previousCommand = currentCommand;
        }
        private static void ValidateAndParseToUserCommands(string userCommand)
        {
            if (Enum.IsDefined(typeof(UserCommands), userCommand))
            {
                currentCommand = (UserCommands)Enum.Parse(typeof(UserCommands), userCommand, ignoreCase: true);
            }
            else
            {
                ChessMessages.OutPutErrorMessages(Errors.WrongCommand);
            }
        }

        public static void InitializeUsersCommands()
        {
            ChessOutPut.ChessTableOutPut();

            Console.Write("Enter the command: ");

            userCommand = Console.ReadLine().ToLower();


            ChessDataValidation.CheckIsPieceChosen(userCommand);
            ChessOutPut.DisableHighLighting();
            ValidateAndParseToUserCommands(userCommand);
            ProcessUsersCommand(currentCommand);

        }

        public static void ProcessFreeModeCommands()
        {


            ChessOutPut.ChessTableOutPut();

            Console.Write("Enter the command: ");

            userCommand = Console.ReadLine().ToLower();
            ValidateAndParseToUserCommands(userCommand);


            switch (currentCommand)
            {
                case UserCommands.back:
                    {
                        ChessOutPut.DisableHighLighting();
                        ProcessBackCommand();

                    }
                    break;

                case UserCommands.create:
                    {
                        ChessMessages.OutPutMakingPieceMessage();
                    }
                    break;


                case UserCommands.delete:
                    {
                        ChessMessages.OutPutDeletionMessage();
                    }
                    break;
                case UserCommands.start:
                    {


                    }
                    break;


                default:

                    ChessMessages.OutPutWrongCommandMessage();
                    ProcessFreeModeCommands();

                    break;
            }
            previousCommand = currentCommand;
        }

        public static void ProcessPieceMovementCommand(string userCommand, string currentPiece)
        {
            var convertedCoordinates = ChessDataValidation.ConvertCoordinates(currentPiece);
            var i = Convert.ToInt32(convertedCoordinates[0].ToString());
            var j = Convert.ToInt32(convertedCoordinates[1].ToString());


            var chessPiece = ChessTable.GetChessCell(i, j).ChessPiece;



            ChessOutPut.HighLightPossibleMoves(chessPiece);

            ChessOutPut.ChessTableOutPut();

            Console.WriteLine($"You've chosen: {currentPiece}");

            Console.Write("Enter the command: ");

     

            userCommand = Console.ReadLine().ToLower();
            ValidateAndParseToUserCommands(userCommand);

            switch (currentCommand)
            {

                case UserCommands.move:
                    {
                        Console.Write("Enter new coordinates: ");

                        var newCoordinates = Console.ReadLine().ToLower();


                        var errorMessageAfterValidation = ChessDataValidation.CheckFormatFalidation(newCoordinates);
                        ChessMessages.OutPutErrorMessages(errorMessageAfterValidation);

                        newCoordinates = ChessDataValidation.ConvertCoordinates(newCoordinates);


                        i = Convert.ToInt32(newCoordinates[0].ToString());
                        j = Convert.ToInt32(newCoordinates[1].ToString());

                        ChessDataValidation.IsSquareTakenByAlly(chessPiece, i, j);
                        ChessDataValidation.ChechIsMoveAvailble(chessPiece, i, j);


                        ChessOutPut.DisableHighLighting();

                        InitializeUsersCommands();


                    }
                    break;

                case UserCommands.place:
                    {
                     Console.Write("Enter new coordinates: ");

                        ChessOutPut.DisableHighLighting();
                        var newCoordinates = Console.ReadLine().ToLower();

                        var errorMessageAfterValidation = ChessDataValidation.CheckFormatFalidation(newCoordinates);
                        ChessMessages.OutPutErrorMessages(errorMessageAfterValidation);

                        newCoordinates = ChessDataValidation.ConvertCoordinates(newCoordinates);
                        i = Convert.ToInt32(newCoordinates[0].ToString());

                        j = Convert.ToInt32(newCoordinates[1].ToString());
                        chessPiece.Move(chessPiece, i, j);

                        InitializeUsersCommands();
                    }

                    break;
                case UserCommands.back:
                    {
                        ChessOutPut.DisableHighLighting();
                        ProcessBackCommand();
                    }
                    break;



                default:

                    ChessMessages.OutPutWrongCommandMessage();
                    ProcessBackCommand();
                

                    break;
            }

            previousCommand = currentCommand;
            ProcessBackCommand();

        }


       public static void ProcessBackCommand()
        {
       
            if (previousCommand == UserCommands.fm || previousCommand == UserCommands.ng || previousCommand == UserCommands.start ||
              previousCommand == UserCommands.clear)
            {
                InitializeUsersCommands();
            }

            if (previousCommand == UserCommands.move || previousCommand == UserCommands.create || previousCommand == UserCommands.delete )
             
            {
                ProcessFreeModeCommands();
            }

        }

    }

    
}
    