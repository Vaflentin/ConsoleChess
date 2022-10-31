using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

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

    public abstract class ChessPiece
    {
        protected List<ChessCells> _validCells = new List<ChessCells>();
        protected List<ChessPiece> _attactedEnemies= new List<ChessPiece>();
        protected List<ChessPiece> _protectedAllies = new List<ChessPiece>();
        protected List<ChessPiece> _allEnemiesOnTheWay = new List<ChessPiece>();

        protected bool _isFirstMove = true;
        private string _pieceName;
        protected bool _isProtected;

        public int I { get; protected set; }
        public int J { get; protected set; }
        public bool IsWhite { get; private set; }

        public List<ChessPiece> AllEnemiesOnTheWay
        {
            get { return _allEnemiesOnTheWay; }
            set { _allEnemiesOnTheWay = value; }
        }

        public List<ChessPiece> ProtectedAllies
        {
            get { return _protectedAllies; }
            set { _protectedAllies = value; }
        }

        public List<ChessPiece> AttactedEnemies
        {
            get { return _attactedEnemies; }
            set { _attactedEnemies = value; }
        }

        public List<ChessCells> VallidCells
        {
            get { return _validCells; }
            set { _validCells = value; }

        }

        //protected void AddEnemy(ChessCells cell)
        //{

        //}
       public void ClearAllPieceLists()
        {
            VallidCells.Clear();
            AttactedEnemies.Clear();
            ProtectedAllies.Clear();
            AllEnemiesOnTheWay.Clear();
        }
        abstract public void CheckPiecesOnTheWay();

      
        public void Capture(ChessPiece chessPiece, int i, int j)
        {

           Move(chessPiece, i, j);
            
        }

       protected static List<ChessCells> RemoveCellWithPieceFromList(List<ChessCells> pieceList)
        {
            pieceList.RemoveAll(cell => cell.HasPiece);

            return pieceList;
        }

        protected static void DeleteNullList(List<List<ChessCells>> pieceLists)
        {
            pieceLists.RemoveAll(list => list.Count == 0 || list == null);
        }

        public void Move(ChessPiece chessPiece, int i, int j)
        {
          
                ChessPiece currentPiece = chessPiece;
                ChessTable.DeletePiece(chessPiece.I, chessPiece.J);

                currentPiece.I = i;
                currentPiece.J = j;

                _isFirstMove = false;

                ChessTable.PlacePiece(currentPiece);

                ChessTable.SetChessLog(currentPiece.PieceName, currentPiece.I, currentPiece.J);
        }

        public void FillAllAttackedEnmies()
        {
            foreach (var cell in VallidCells)
            {
                if (cell.HasPiece)
                {
                    if (IsWhite && !cell.ChessPiece.IsWhite || !IsWhite && cell.ChessPiece.IsWhite)
                    {
                        _allEnemiesOnTheWay.Add(cell.ChessPiece);
                    }
               
                }
            }
        }


        public List<List<ChessCells>> GetAllListFields()
        {
            var listFields = GetType().GetFields(BindingFlags.Instance| BindingFlags.NonPublic );
            List<List<ChessCells>> allCellLists = new List<List<ChessCells>>();
  
            foreach (var field in listFields)
            {
                   if (field.FieldType.IsGenericType && field.Name != "VallidCells" && field.Name != "vallidCells" && field.GetValue(this) is List<ChessCells>)
                {
                    var currentList = (List <ChessCells>)field.GetValue(this);
                    allCellLists.Add(currentList);
                }
            }

            return allCellLists;
        }

        public void DeleteInvalidCellsFromList()
        {
           _validCells.RemoveAll((cell => !Regex.IsMatch((cell.I.ToString() + cell.J.ToString()), "(?=.{2}$)[0-7][0-7]+")));
            _validCells.RemoveAll(cell => (cell.I == I) && (cell.J == J));
        }

        protected static bool CheckForValidCell(int i, int j, List<ChessCells> validCells)
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

        //public void Replace(ChessPiece chessPiece, int i, int j)
        //{
        //    ChessPiece currentPiece = chessPiece;
           
        //    ChessTable.DeletePiece(chessPiece.I, chessPiece.J);
        //    currentPiece.I = i;
        //    currentPiece.J = j;
        //    ChessTable.PlacePiece(currentPiece);
        //}
        public static Errors SelectPiece(int i, int j, string pieceName)
        {

            var chessCell = ChessTable.GetChessCell(i, j);
            //var piece = chessCell.ChessPiece;

            if (chessCell.HasPiece && chessCell.ChessPiece.PieceName == pieceName)
            {
                //piece.ProduceValidCells();
                //piece.CheckPiecesOnTheWay();
                //if (piece.VallidCells.Count == 0)
                //{
                //    return Errors.NoAnyLegalMove;
                //}
               Player.ProduceAllPieces();


                return Errors.NoErrors;
     
            }


            return Errors.CellDoesNotContainSuchPiece;

        }





        abstract public void ProduceValidCells();
       //public  virtual Errors ValidateSquares(ChessPiece chessPiece, int i, int j)
       // {

       //     if (!ChessDataValidation.CheckValidCellMatch(i, j, chessPiece.VallidCells))
       //     {
       //         return Errors.InvalidSquare;
       //     }

       //     return Errors.NoErrors;
       // }
    


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
