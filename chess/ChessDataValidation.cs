using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace chess
{
    public static class ChessDataValidation
    {
        public static void CheckIsPieceChosen(string userCommand)
        {
          

            var currentPiece = userCommand;


            if ((CheckFormatFalidation(userCommand) == Errors.NoErrors) || (CheckFormatFalidation(userCommand) == Errors.ChessSquareAlreadyTaken))
            {
                userCommand = ConvertCoordinates(userCommand);
                var i = Convert.ToInt32(userCommand[0].ToString());
                var j = Convert.ToInt32(userCommand[1].ToString());
                var pieceName = userCommand[2].ToString();
                //var currentFakeChessTable = ChessOutPut.GetFakeChessTable();
                //var currentCell = currentFakeChessTable[i + 1, j + 1];

                if (ChessPiece.SelectPiece(i, j,pieceName) == Errors.NoErrors)
                {
                    ChessOutPut.DisableHighLighting();
                    ChessCommand.ProcessPieceMovementCommand(userCommand, currentPiece);

                }
            
                else
                {
                    ChessMessages.OutPutErrorMessages(Errors.CellDoesNotContainSuchPiece);
                }
            }


        }


        public static string ConvertCoordinates(string currentPiece)
        {

            var i = (8 - Convert.ToInt32(currentPiece[2].ToString())).ToString();
            var j = (Collumns)Enum.Parse(typeof(Collumns), Convert.ToString(currentPiece[1]).ToLower(), ignoreCase: true);
            var temporalJ = (int)j;

            string convertedCoordinates = i + temporalJ.ToString();
            convertedCoordinates = convertedCoordinates + currentPiece[0];


            return convertedCoordinates;
        }



        private static Errors IsSquareTaken(int i, int j)
        {
            if (!ChessCells.GetCellCondition(i, (int)j))
            {
                return Errors.NoErrors;
            }
            return Errors.ChessSquareAlreadyTaken;
        }

        public static Errors CheckFormatFalidation(string playersPiece)
        {


            try
            {
                if (Regex.IsMatch(playersPiece, "(?=.{3}$)[pbqnkrPBQNKR][a-hA-H][1-8]"))
                {
                    var convertedCoordinates = ConvertCoordinates(playersPiece);
                    var i = Convert.ToInt32(convertedCoordinates[0].ToString());
                    var j = Convert.ToInt32(convertedCoordinates[1].ToString());


                    return IsSquareTaken(i, (int)j);
                }

                return Errors.InvalidSquare;
            }
            catch (Exception)
            {
                return Errors.UnknownExcpetion;

                throw;
            }



        }

    }
}
