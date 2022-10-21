using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace chess
{


    public abstract class ChessPiece
    {

        protected List<ChessCells> validCells = new List<ChessCells>();
        public List<ChessCells> VallidCells
        {
            get { return validCells; }
            set { validCells = value; }


        }

        public bool IsWhite { get; private set; }

        private string _pieceName;
        public int I { get; protected set; }
        public int J { get; protected set; }

 


        abstract public void CheckPiecesOnTheWay(ChessPiece chessPiece);



      protected static List<List<ChessCells>> GetAllListFields(ChessPiece chessPiece)
        {
            var listFields =  chessPiece.GetType().GetFields(BindingFlags.Instance| BindingFlags.NonPublic );
            List<List<ChessCells>> allCellLists = new List<List<ChessCells>>();
  
            
            foreach (var field in listFields)
            {
   
                if (field.FieldType.IsGenericType)
                {
                    var currentList = (List < ChessCells >) field.GetValue(chessPiece);
                    allCellLists.Add(currentList);
                }
            }

            return allCellLists;
        }

        public static void DeleteInvalidCellsFromList(ChessPiece chessPiece)
        {



            chessPiece.validCells.RemoveAll((cell => !Regex.IsMatch((cell.I.ToString() + cell.J.ToString()), "(?=.{2}$)[0-7][0-7]+")));

            chessPiece.validCells.RemoveAll(cell => (cell.I == chessPiece.I) && (cell.J == chessPiece.J));

        }
        protected static bool ProcessValidCells(int i, int j, List<ChessCells> validCells)
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

        public void Replace(ChessPiece chessPiece, int i, int j)
        {
            ChessPiece currentPiece = chessPiece;

            ChessTable.DeletePiece(chessPiece.I, chessPiece.J);
            currentPiece.I = i;
            currentPiece.J = j;
            ChessTable.PlacePiece(currentPiece);
        }
        public static Errors SelectPiece(int i, int j)
        {

            var currentTable = ChessTable.ChessCells;
            var chessCell = currentTable[i, j];
            var piece = chessCell.ChessPiece;

            if (!chessCell.HasPiece)
            {
                return Errors.CellDoesNotContainSuchPiece;
               

            }
            else
            {
            piece.ProduceValidCells(piece);
            return Errors.NoErrors;
            }
          
              
           
        }



        public void Move(ChessPiece chessPiece, int i, int j)
        {

            if (ValidateSquares(chessPiece, i, j) == Errors.NoErrors)
            {

                ChessPiece currentPiece = chessPiece;

                ChessTable.DeletePiece(chessPiece.I, chessPiece.J);


                currentPiece.I = i;
                currentPiece.J = j;

                ChessTable.PlacePiece(currentPiece);

                ChessTable.SetChessLog(currentPiece.PieceName, currentPiece.I, currentPiece.J);
            }
            else
            {
                ChessMessages.OutPutErrorMessages(Errors.InvalidSquare);
            }

        }


        abstract public void ProduceValidCells(ChessPiece chessPiece);
        abstract public Errors ValidateSquares(ChessPiece chessPiece, int i, int j);


        public ChessPiece(int i, int j, string pieceName, bool isWhite)
        {

            {
                IsWhite = isWhite;
                _pieceName = pieceName;
                I = i;
                J = j;
            }

        }


        public string PieceName
        {
            get
            {
                return _pieceName;
            }
            private set
            {
                _pieceName = value;
            }
        }

    }
   


}
