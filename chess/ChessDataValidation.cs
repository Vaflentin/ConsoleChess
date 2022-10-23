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
        public static Errors ValidateSquares(ChessPiece chessPiece, int i, int j)
        {

            if (!CheckValidCellMatch(i, j, chessPiece.VallidCells))
            {
                return Errors.InvalidSquare;
            }

            return Errors.NoErrors;
        }

        public static void ChechIsMoveAvailble(ChessPiece chessPiece, int i, int j)
        {
            if (ValidateSquares(chessPiece, i, j) == Errors.NoErrors)
            {
                chessPiece.Move(chessPiece, i, j);
               
            }
            else
            {
                ChessMessages.OutPutErrorMessages(Errors.InvalidSquare);
            }
        }

        public static bool CheckValidCellMatch(int i, int j, List<ChessCells> validCells)
        {

            foreach (ChessCells cell in validCells)
            {
                if (cell.I == i && cell.J == j)
                {
                    return true;
                }
            }
            return false;
        }
        public static void CheckIsPieceChosen(string userCommand)
        {
          

            var currentPiece = userCommand;


            if ((CheckFormatFalidation(userCommand) == Errors.NoErrors) || (CheckFormatFalidation(userCommand) == Errors.ChessSquareAlreadyTaken))
            {
                userCommand = ConvertCoordinates(userCommand);
                var i = Convert.ToInt32(userCommand[0].ToString());
                var j = Convert.ToInt32(userCommand[1].ToString());
                var pieceName = userCommand[2].ToString();
                Errors error = ChessPiece.SelectPiece(i, j, pieceName);
                //var currentFakeChessTable = ChessOutPut.GetFakeChessTable();
                //var currentCell = currentFakeChessTable[i + 1, j + 1];

                switch (error)
                {
                    case Errors.NoErrors:
                        {
                            ChessOutPut.DisableHighLighting();
                            ChessCommand.ProcessPieceMovementCommand(userCommand, currentPiece);
                        }
                        break;

                    case Errors.CellDoesNotContainSuchPiece:
                        {
                            ChessMessages.OutPutErrorMessages(Errors.CellDoesNotContainSuchPiece);
                        }
                        break;
                    case Errors.NoAnyLegalMove:
                        {
                            ChessOutPut.DisableHighLighting();
                            ChessCommand.ProcessPieceMovementCommand(userCommand, currentPiece);
                        }
                        break;
         
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


        //public static void AddEnemiesToCapture(ChessPiece chessPiece)
        //{
        //    chessPiece.VallidCells.AddRange((IEnumerable<ChessCells>)chessPiece.AttactedEnemies);

        //    //foreach (var piece in chessPiece.AttactedEnemies)
        //    //{
        //    //    if (piece.I == i && piece.J == j)
        //    //    {
        //    //        return Errors.NoErrors;
        //    //    }
        //    //}

        //    //return Errors.InvalidSquare;
        //}

        public static void IsSquareTakenByAlly(ChessPiece chessPiece,int i, int j)
        {
            var cell = ChessTable.GetChessCell(i, j);
    
            if (cell.HasPiece)
            {
                var supAlly = cell.ChessPiece;

                if (chessPiece.IsWhite && supAlly.IsWhite || !chessPiece.IsWhite && !supAlly.IsWhite)
                {
                    ChessMessages.OutPutErrorMessages(Errors.ChessSquareAlreadyTaken);
                }
            }
            
     
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


                    return Errors.NoErrors;
                }

                return Errors.InvalidFormat;
            }
            catch (Exception)
            {
                return Errors.UnknownExcpetion;

                throw;
            }



        }

    }
}
