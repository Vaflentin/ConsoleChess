using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace chess
{
    class Queen : ChessPiece
    {
        protected List<ChessCells> diagonalUpperLeftValidCells = new List<ChessCells>();
        protected List<ChessCells> diagonalUpperRightValidCells = new List<ChessCells>();
        protected List<ChessCells> diagonalLowerLeftValidCells = new List<ChessCells>();
        protected List<ChessCells> diagonalLowerRightValidCells = new List<ChessCells>();

        //protected List<ChessCells> straightValidCells = new List<ChessCells>();

        protected List<ChessCells> straightUpLineValidCells = new List<ChessCells>();
        protected List<ChessCells> straightDownLineValidCells = new List<ChessCells>();
        protected List<ChessCells> straightLeftLineValidCells = new List<ChessCells>();
        protected List<ChessCells> straightRightLineValidCells = new List<ChessCells>();

        public Queen(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

      
        protected void ProduceDiagonalCells()
        {
            diagonalLowerLeftValidCells.Clear();
            diagonalLowerRightValidCells.Clear();
            diagonalUpperLeftValidCells.Clear();
            diagonalUpperRightValidCells.Clear();




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
                            diagonalLowerRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));
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
                        diagonalLowerLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

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
                        diagonalUpperRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

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
                        diagonalUpperLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

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

                if ( member.GetValue(this) is IList<ChessCells> && member.GetValue(this) != VallidCells)
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
        public void MergeVallidCellsArray(List<ChessCells> baseCellList ,params List<ChessCells>[] Lists)
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

            straightDownLineValidCells.Clear();
            straightUpLineValidCells.Clear();
            straightLeftLineValidCells.Clear();
            straightRightLineValidCells.Clear();



            if (I - 1 >= 0) // вверх
            {
                for (int i = I - 1; i >= 0; i--)
                {
                    straightUpLineValidCells.Add(ChessTable.GetChessCell(i, J));
                }
            }

            if (I + 1 < ChessTable.ChessCells.GetLength(0)) // вниз
            {
                for (int i = I + 1; i < ChessTable.ChessCells.GetLength(0); i++)
                {
                    straightDownLineValidCells.Add(ChessTable.GetChessCell(i, J));
                }
            }


            if (J - 1 >= 0) // влево
            {
                for (int j = J - 1; j >= 0; j--)
                {
                    straightLeftLineValidCells.Add(ChessTable.GetChessCell(I, j));
                }

            }

            if (J + 1 < ChessTable.ChessCells.GetLength(1)) // вправо
            {
                for (int j = J + 1; j < ChessTable.ChessCells.GetLength(1); j++)
                {
                    straightRightLineValidCells.Add(ChessTable.GetChessCell(I, j));
                }

            }


        }


        public override void ProduceValidCells()
        {
        
            ProduceDiagonalCells();
            ProduceStraightCells();
            MergeVallidCellsArray();

            FillAllAttackedEnmies();

            //CheckPiecesOnTheWay();

        }





        protected  void ProcessPiecesOnTheWay(List<ChessCells> list) // todo:: Надо зарефакторить - упростить код
        {
            bool isRangeOFCellAlreadyRemoved;
            ComparerI comparerI = new ComparerI();
            //ComparerJ comparerJ = new ComparerJ();
            List<ChessCells> tempolarCells = new List<ChessCells>(list);

            if (list == straightUpLineValidCells || list == straightLeftLineValidCells || list == diagonalUpperLeftValidCells || list == diagonalUpperRightValidCells)
            {
                tempolarCells.Sort(comparerI);
                list.Sort(comparerI);


            }

            if (list.Count != 0)
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
            var allListOfValidCells = GetAllListFields();

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
