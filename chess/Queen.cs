//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reflection;

//namespace chess
//{
//    class Queen : ChessPiece
//    {
//        protected List<ChessCells> diagonalUpperLeftValidCells = new List<ChessCells>();
//        protected List<ChessCells> diagonalUpperRightValidCells = new List<ChessCells>();
//        protected List<ChessCells> diagonalLowerLeftValidCells = new List<ChessCells>();
//        protected List<ChessCells> diagonalLowerRightValidCells = new List<ChessCells>();

//        //protected List<ChessCells> straightValidCells = new List<ChessCells>();

//        protected List<ChessCells> straightUpLineValidCells = new List<ChessCells>();
//        protected List<ChessCells> straightDownLineValidCells = new List<ChessCells>();
//        protected List<ChessCells> straightLeftLineValidCells = new List<ChessCells>();
//        protected List<ChessCells> straightRightLineValidCells = new List<ChessCells>();

//        public Queen(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
//        {

//        }


//        protected void ProduceDiagonalCells()
//        {
//            diagonalLowerLeftValidCells.Clear();
//            diagonalLowerRightValidCells.Clear();
//            diagonalUpperLeftValidCells.Clear();
//            diagonalUpperRightValidCells.Clear();




//            int currentI = I;
//            int currentJ = J;

//            //Нижняя правая диагональ

//            for (int column = I + 1; column < ChessTable.ChessCells.GetLength(0); column++)
//            {
//                for (int row = J + 1; row < ChessTable.ChessCells.GetLength(1); row++)
//                {
//                    if ((column - currentI == 1) && (row - currentJ == 1))
//                    {

//                        currentI = column;
//                        currentJ = row;
//                        //chessPiece.diagonalLowerRightValidCells.Add(currentI.ToString() + currentJ.ToString());
//                        if ((currentI < 8 && currentI >= 0))
//                        {
//                            diagonalLowerRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));
//                        }


//                    }



//                }

//            }

//            currentI = I;
//            currentJ = J;

//            //Нижняя левая диагональ

//            for (int column = I + 1; column < ChessTable.ChessCells.GetLength(0); column++)
//            {
//                for (int row = J - 1; row >= 0; row--)
//                {
//                    if ((column - currentI == 1) && (row - currentJ == -1))
//                    {

//                        currentI = column;
//                        currentJ = row;
//                        diagonalLowerLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

//                    }

//                }
//            }

//            currentI = I;
//            currentJ = J;

//            //Верхняя правая диагональ

//            for (int column = I - 1; column >= 0; column--)
//            {
//                for (int row = J + 1; row < ChessTable.ChessCells.GetLength(1); row++)
//                {
//                    if ((currentI - column == 1) && (currentJ - row == -1))
//                    {

//                        currentI = column;
//                        currentJ = row;
//                        diagonalUpperRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

//                    }

//                }
//            }

//            currentI = I;
//            currentJ = J;

//            //Верхняя левая диагональ

//            for (int column = I - 1; column >= 0; column--)
//            {
//                for (int row = J - 1; row >= 0; row--)
//                {

//                    if ((currentI - column == 1) && (row - currentJ == -1))
//                    {

//                        currentI = column;
//                        currentJ = row;
//                        diagonalUpperLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

//                    }
//              }

//            }



//        }



//        public void MergeVallidCellsArray()
//        {
//          VallidCells.Clear();


//            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

//            foreach (var member in fields)
//            {

//                if ( member.GetValue(this) is IList<ChessCells> && member.GetValue(this) != VallidCells)
//                {
//                   List<ChessCells> list = (List<ChessCells>)member.GetValue(this);
//                    if (list.Count != 0)
//                    {
//                        var memberValue = member.GetValue(this);
//                        List<ChessCells> currentList = (List<ChessCells>)memberValue;

//                        VallidCells.AddRange(currentList);
//                    }

//                }


//            }



//        }
//        public void MergeVallidCellsArray(List<ChessCells> baseCellList ,params List<ChessCells>[] Lists)
//        {
//            if (baseCellList != null)
//            {
//                baseCellList.Clear();

//                for (int i = 0; i < Lists.Length; i++)
//                {
//                    baseCellList.AddRange(Lists[i]);
//                }
//            }

//        }


//        protected void ProduceStraightCells()
//        {

//            straightDownLineValidCells.Clear();
//            straightUpLineValidCells.Clear();
//            straightLeftLineValidCells.Clear();
//            straightRightLineValidCells.Clear();



//            if (I - 1 >= 0) // вверх
//            {
//                for (int i = I - 1; i >= 0; i--)
//                {
//                    straightUpLineValidCells.Add(ChessTable.GetChessCell(i, J));
//                }
//            }

//            if (I + 1 < ChessTable.ChessCells.GetLength(0)) // вниз
//            {
//                for (int i = I + 1; i < ChessTable.ChessCells.GetLength(0); i++)
//                {
//                    straightDownLineValidCells.Add(ChessTable.GetChessCell(i, J));
//                }
//            }


//            if (J - 1 >= 0) // влево
//            {
//                for (int j = J - 1; j >= 0; j--)
//                {
//                    straightLeftLineValidCells.Add(ChessTable.GetChessCell(I, j));
//                }

//            }

//            if (J + 1 < ChessTable.ChessCells.GetLength(1)) // вправо
//            {
//                for (int j = J + 1; j < ChessTable.ChessCells.GetLength(1); j++)
//                {
//                    straightRightLineValidCells.Add(ChessTable.GetChessCell(I, j));
//                }

//            }


//        }


//        public override void ProduceValidCells()
//        {

//            ProduceDiagonalCells();
//            ProduceStraightCells();
//            MergeVallidCellsArray();

//            FillAllAttackedEnmies();

//            //CheckPiecesOnTheWay();

//        }





//        protected  void ProcessPiecesOnTheWay(List<ChessCells> list) // todo:: Надо зарефакторить - упростить код
//        {
//            bool isRangeOFCellAlreadyRemoved;
//            ComparerI comparerI = new ComparerI();
//            //ComparerJ comparerJ = new ComparerJ();
//            List<ChessCells> tempolarCells = new List<ChessCells>(list);

//            if (list == straightUpLineValidCells || list == straightLeftLineValidCells || list == diagonalUpperLeftValidCells || list == diagonalUpperRightValidCells)
//            {
//                tempolarCells.Sort(comparerI);
//                list.Sort(comparerI);


//            }

//            if (list.Count != 0 )
//            {

//                isRangeOFCellAlreadyRemoved = false;

//                foreach (var cell in tempolarCells)
//                {

//                    if (!isRangeOFCellAlreadyRemoved)
//                    {
//                        if (list.Count != 0 && cell.HasPiece)
//                        {
//                            if (IsWhite)
//                            {
//                                if (cell.ChessPiece.IsWhite)
//                                {
//                                    _protectedAllies.Add(cell.ChessPiece);
//                                    var currentCellIndex = list.IndexOf(cell);
//                                    list.RemoveRange(currentCellIndex, list.Count - currentCellIndex);

//                                }
//                                if (!cell.ChessPiece.IsWhite)
//                                {
//                                    int cellIndex;

//                                    if (list.IndexOf(cell) + 1! <= list.Count)
//                                    {
//                                        cellIndex = list.IndexOf(cell) + 1;
//                                    }
//                                    else cellIndex = list.IndexOf(cell);


//                                    list.RemoveRange(cellIndex, list.Count - cellIndex);
//                                   _attactedEnemies.Add(cell.ChessPiece);
//                                }
//                            }
//                            else
//                            {
//                                if (!cell.ChessPiece.IsWhite)
//                                {
//                                  _protectedAllies.Add(cell.ChessPiece);
//                                    var currentCellIndex = list.IndexOf(cell);
//                                    list.RemoveRange(currentCellIndex, list.Count - currentCellIndex);

//                                }
//                                if (cell.ChessPiece.IsWhite)
//                                {
//                                    int cellIndex;

//                                    if (list.IndexOf(cell) + 1! <= list.Count)
//                                    {
//                                        cellIndex = list.IndexOf(cell) + 1;
//                                    }
//                                    else cellIndex = list.IndexOf(cell);
//                                    _attactedEnemies.Add(cell.ChessPiece);

//                                    list.RemoveRange(cellIndex, list.Count - cellIndex);

//                                }
//                            }


//                            isRangeOFCellAlreadyRemoved = true;

//                        }
//                    }

//                    else break;


//                }

//            }

//        }

//        protected void SortThroughCellLists(List<List<ChessCells>> allLists)
//        {
//            foreach (var list in allLists)
//            {
//                ProcessPiecesOnTheWay(list);
//            }
//        }
//        protected virtual void SortThroughCellLists()
//        {
//            var allListOfValidCells = GetAllListFields();

//            foreach (var list in allListOfValidCells)
//            {
//                ProcessPiecesOnTheWay(list);
//            }

//        }
//        public override void CheckPiecesOnTheWay()
//        {

//            SortThroughCellLists();
//            //MergeVallidCellsArray();
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace chess
{
   public class Queen : ChessPiece
    {
        //protected List<ChessCells> straightValidCells = new List<ChessCells>();

        protected List<ChessCells> _diagonalUpperLeftValidCells = new List<ChessCells>();
        protected List<ChessCells> _diagonalUpperRightValidCells = new List<ChessCells>();
        protected List<ChessCells> _diagonalLowerLeftValidCells = new List<ChessCells>();
        protected List<ChessCells> _diagonalLowerRightValidCells = new List<ChessCells>();

        protected List<ChessCells> _upperFileValidCells = new List<ChessCells>();
        protected List<ChessCells> _lowerFileValidCells = new List<ChessCells>();
        protected List<ChessCells> _leftRankValidCells = new List<ChessCells>();
        protected List<ChessCells> _rightRankValidCells = new List<ChessCells>();

        protected List<ChessPiece> _upperLeftDiagonalyEnemies = new();
        protected List<ChessPiece> _upperRightDiagonalEnemies = new();
        protected List<ChessPiece> _lowerLeftDiagonalEnemies = new();
        protected List<ChessPiece> _lowerRightDiagonalEnemies = new();

        protected List<ChessPiece> _upperFileEnemies = new();
        protected List<ChessPiece> _lowerFileEnemies = new();
        protected List<ChessPiece> _leftRankEnemies = new();
        protected List<ChessPiece> _rightRankEnemies = new();

        public Queen(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }


        protected void ProduceDiagonalCells()
        {
            _diagonalLowerLeftValidCells.Clear();
            _diagonalLowerRightValidCells.Clear();
            _diagonalUpperLeftValidCells.Clear();
            _diagonalUpperRightValidCells.Clear();

           


            int currentI = I;
            int currentJ = J;

            //Нижняя правая диагональ

            for (int column = I + 1; column < ChessTable.ChessCells.GetLength(0); column++)
            {
                for (int row = J + 1; row < ChessTable.ChessCells.GetLength(1); row++)
                {
                    if ((column - currentI == 1) && (row - currentJ == 1))
                    {

                        currentI = column;
                        currentJ = row;
                        //chessPiece.diagonalLowerRightValidCells.Add(currentI.ToString() + currentJ.ToString());
                        if ((currentI < 8 && currentI >= 0))
                        {
                            _diagonalLowerRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));
                        }


                    }



                }

            }

            currentI = I;
            currentJ = J;

            //Нижняя левая диагональ

            for (int column = I + 1; column < ChessTable.ChessCells.GetLength(0); column++)
            {
                for (int row = J - 1; row >= 0; row--)
                {
                    if ((column - currentI == 1) && (row - currentJ == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        _diagonalLowerLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

                    }

                }
            }

            currentI = I;
            currentJ = J;

            //Верхняя правая диагональ

            for (int column = I - 1; column >= 0; column--)
            {
                for (int row = J + 1; row < ChessTable.ChessCells.GetLength(1); row++)
                {
                    if ((currentI - column == 1) && (currentJ - row == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        _diagonalUpperRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

                    }

                }
            }

            currentI = I;
            currentJ = J;

            //Верхняя левая диагональ

            for (int column = I - 1; column >= 0; column--)
            {
                for (int row = J - 1; row >= 0; row--)
                {

                    if ((currentI - column == 1) && (row - currentJ == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        _diagonalUpperLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

                    }
                }

            }



        }



        public void MergeVallidCellsArray()
        {
            VallidCells.Clear();


            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var member in fields)
            {

                if (member.GetValue(this) is IList<ChessCells> && member.GetValue(this) != VallidCells)
                {
                    List<ChessCells> list = (List<ChessCells>)member.GetValue(this);
                    if (list.Count != 0)
                    {
                        var memberValue = member.GetValue(this);
                        List<ChessCells> currentList = (List<ChessCells>)memberValue;

                        VallidCells.AddRange(currentList);
                    }

                }


            }



        }
        public void MergeVallidCellsArray(List<ChessCells> baseCellList, params List<ChessCells>[] Lists)
        {
            if (baseCellList != null)
            {
                baseCellList.Clear();

                for (int i = 0; i < Lists.Length; i++)
                {
                    baseCellList.AddRange(Lists[i]);
                }
            }

        }


        protected void ProduceStraightCells()
        {

            _lowerFileValidCells.Clear();
            _upperFileValidCells.Clear();
            _leftRankValidCells.Clear();
            _rightRankValidCells.Clear();



            if (I - 1 >= 0) // вверх
            {
                for (int i = I - 1; i >= 0; i--)
                {
                    _upperFileValidCells.Add(ChessTable.GetChessCell(i, J));
                }
            }

            if (I + 1 < ChessTable.ChessCells.GetLength(0)) // вниз
            {
                for (int i = I + 1; i < ChessTable.ChessCells.GetLength(0); i++)
                {
                    _lowerFileValidCells.Add(ChessTable.GetChessCell(i, J));
                }
            }


            if (J - 1 >= 0) // влево
            {
                for (int j = J - 1; j >= 0; j--)
                {
                    _leftRankValidCells.Add(ChessTable.GetChessCell(I, j));
                }

            }

            if (J + 1 < ChessTable.ChessCells.GetLength(1)) // вправо
            {
                for (int j = J + 1; j < ChessTable.ChessCells.GetLength(1); j++)
                {
                    _rightRankValidCells.Add(ChessTable.GetChessCell(I, j));
                }

            }


        }


        public override void ProduceValidCells()
        {

            ProduceDiagonalCells();
            ProduceStraightCells();

            AddAllEnemiesOnDiagonals();
            AddAllEnemiesOnFilesAndRanks();
            //CheckPiecesOnTheWay();
            //MergeVallidCellsArray();

            FillAllAttackedEnmies();

      

        }

        private void RemoveAlliesPieces(params List<ChessPiece>[] lists)
        {

            foreach (var pieceList in lists)
            {
             
                if (this.IsWhite)
                {
                    pieceList.RemoveAll(piece => piece.IsWhite);

                }
                else
                {
                    pieceList.RemoveAll(piece => !piece.IsWhite);
                }
            }
        }
        protected void AddAllEnemiesOnFilesAndRanks()
        {
       
                _upperFileEnemies = DeleteNoPieceCells(_upperFileValidCells);
                _lowerFileEnemies = DeleteNoPieceCells(_lowerFileValidCells);
                _rightRankEnemies = DeleteNoPieceCells(_rightRankValidCells);
                _leftRankEnemies = DeleteNoPieceCells(_leftRankValidCells);

            RemoveAlliesPieces(_upperFileEnemies, _lowerFileEnemies, _rightRankEnemies, _leftRankEnemies);
         
      
        }
        protected void AddAllEnemiesOnDiagonals()
        {

             _upperLeftDiagonalyEnemies = DeleteNoPieceCells(_diagonalUpperLeftValidCells);
             _upperRightDiagonalEnemies = DeleteNoPieceCells(_diagonalUpperRightValidCells);
             _lowerLeftDiagonalEnemies = DeleteNoPieceCells(_diagonalLowerLeftValidCells);
             _lowerRightDiagonalEnemies = DeleteNoPieceCells(_diagonalLowerRightValidCells);

            RemoveAlliesPieces(_upperLeftDiagonalyEnemies, _upperRightDiagonalEnemies, _lowerLeftDiagonalEnemies, _lowerRightDiagonalEnemies);
        }




        protected void ProcessPiecesOnTheWay(List<ChessCells> list) // todo:: Надо зарефакторить - упростить код
        {
            bool isRangeOFCellAlreadyRemoved;
            ComparerI comparerI = new ComparerI();
            //ComparerJ comparerJ = new ComparerJ();
            List<ChessCells> tempolarCells = new List<ChessCells>(list);

            if (list == _upperFileValidCells || list == _leftRankValidCells || list == _diagonalUpperLeftValidCells || list == _diagonalUpperRightValidCells)
            {
                tempolarCells.Sort(comparerI);
                list.Sort(comparerI);


            }

            if (list.Count != 0 && list != VallidCells)
            {


                isRangeOFCellAlreadyRemoved = false;

                foreach (var cell in tempolarCells)
                {

                    if (!isRangeOFCellAlreadyRemoved)
                    {
                        if (list.Count != 0 && cell.HasPiece)
                        {
                            if (IsWhite)
                            {
                                if (cell.ChessPiece.IsWhite)
                                {
                                    _protectedAllies.Add(cell.ChessPiece);
                                    var currentCellIndex = list.IndexOf(cell);
                                    list.RemoveRange(currentCellIndex, list.Count - currentCellIndex);

                                }
                                if (!cell.ChessPiece.IsWhite)
                                {
                                    int cellIndex;

                                    if (list.IndexOf(cell) + 1! <= list.Count)
                                    {
                                        cellIndex = list.IndexOf(cell) + 1;
                                    }
                                    else cellIndex = list.IndexOf(cell);


                                    list.RemoveRange(cellIndex, list.Count - cellIndex);
                                    _attactedEnemies.Add(cell.ChessPiece);
                                }
                            }
                            else
                            {
                                if (!cell.ChessPiece.IsWhite)
                                {
                                    _protectedAllies.Add(cell.ChessPiece);
                                    var currentCellIndex = list.IndexOf(cell);
                                    list.RemoveRange(currentCellIndex, list.Count - currentCellIndex);

                                }
                                if (cell.ChessPiece.IsWhite)
                                {
                                    int cellIndex;

                                    if (list.IndexOf(cell) + 1! <= list.Count)
                                    {
                                        cellIndex = list.IndexOf(cell) + 1;
                                    }
                                    else cellIndex = list.IndexOf(cell);
                                    _attactedEnemies.Add(cell.ChessPiece);

                                    list.RemoveRange(cellIndex, list.Count - cellIndex);

                                }
                            }


                            isRangeOFCellAlreadyRemoved = true;

                        }
                    }

                    else break;


                }

            }

        }


        protected virtual void SortThroughCellLists()
        {
            var allListOfValidCells = GetAllChessCellsListFields();

            foreach (var list in allListOfValidCells)
            {
                ProcessPiecesOnTheWay(list);
            }

        }
        public override void CheckPiecesOnTheWay()
        {

            SortThroughCellLists();
            MergeVallidCellsArray();

       
        }
    }
}
