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

        //public static void CheckAlliesOnTheWay(ChessPiece chessPiece)
        //{

        //    switch (chessPiece)
        //    {
        //        case Queen:

        //            {
        //                if (chessPiece.PieceName == "b" || chessPiece.PieceName == "q")
        //                {
        //                    Queen currentChessPiece = (Queen)chessPiece;
        //                    foreach (var item in currentChessPiece.)
        //                    {

        //                    }
        //                }
        //            }

        //            break;


        //        case King:

        //            break;

        //        case Pawn:

        //            break;

        //        default:
        //            break;
        //    }
        //}


        abstract public void CheckAlliesOnTheWay(ChessPiece chessPiece);





        public static void DeleteInvalidCellsFromList(ChessPiece chessPiece)
        {



            chessPiece.validCells.RemoveAll((cell => !Regex.IsMatch((cell.I.ToString() + cell.J.ToString()), "(?=.{2}$)[0-7][0-7]+")));

            chessPiece.validCells.RemoveAll(cell => (cell.I == chessPiece.I) && (cell.J == chessPiece.J));

        }
        protected static bool ProcessValidCells(int i, int j, List<ChessCells> diagonal)
        {

            foreach (var diagonalsCell in diagonal)
            {
                if (diagonalsCell.I == i && diagonalsCell.J == j)
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
    class Pawn : ChessPiece
    {

        private bool isFirstMove = true;




        public Pawn(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void CheckAlliesOnTheWay(ChessPiece chessPiece)
        {
            Pawn pawn = (Pawn)chessPiece;
            ComparerI comparer = new ComparerI();
            List<ChessCells> tempolarValidCell = new List<ChessCells>(pawn.VallidCells);

            int counter = 0;
            if (pawn.IsWhite)
            {



                //tempolarValidCell.Sort(comparer);
                //pawn.VallidCells.Sort(comparer);

                foreach (var cell in tempolarValidCell)
                {




                    if (cell.HasPiece && cell.ChessPiece.IsWhite)
                    {
                        pawn.validCells.RemoveRange(counter, pawn.validCells.Count - counter);
                        if (pawn.validCells.Count == 0)
                        {
                            break;
                        }

                    }

                    counter++;
                }
            }
            if (!pawn.IsWhite)
            {

                tempolarValidCell.Sort((x, y) => x.I.CompareTo(y.I));
                pawn.validCells.Sort((x, y) => x.I.CompareTo(y.I));

                foreach (var cell in tempolarValidCell)
                {


                    if (cell.HasPiece && !cell.ChessPiece.IsWhite)
                    {
                        pawn.validCells.RemoveRange(counter, pawn.validCells.Count - counter);
                    }

                    counter++;
                }

            }
        }

        public override void ProduceValidCells(ChessPiece pawn)
        {
            var currentPawn = (Pawn)pawn;
            currentPawn.validCells.Clear();


            if (pawn.IsWhite)
            {
                if (!currentPawn.isFirstMove)
                {
                    //currentPawn.validCells.Add((currentPawn.I - 1) + currentPawn.J.);
                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - 1, currentPawn.J));
                }
                else
                {
                    //currentPawn.validCells.Add((currentPawn.I - 2).ToString() + currentPawn.J.ToString());
                    //currentPawn.validCells.Add((currentPawn.I - 1).ToString() + currentPawn.J.ToString());

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - 2, currentPawn.J));
                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I - 1, currentPawn.J));
                }
            }
            else
            {
                if (!currentPawn.isFirstMove)
                {
                    //currentPawn.validCells.Add((currentPawn.I + 1).ToString() + currentPawn.J.ToString());

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I + 1, currentPawn.J));
                }
                else
                {
                    //currentPawn.validCells.Add((currentPawn.I + 2).ToString() + currentPawn.J.ToString());
                    //currentPawn.validCells.Add((currentPawn.I + 1).ToString() + currentPawn.J.ToString());

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I + 2, currentPawn.J));

                    currentPawn.validCells.Add(ChessTable.GetChessCell(currentPawn.I + 1, currentPawn.J));

                }
            }



            DeleteInvalidCellsFromList(currentPawn);
        }

        public override Errors ValidateSquares(ChessPiece pawn, int i, int j)
        {
            Pawn currentPawn = (Pawn)pawn;
            ProduceValidCells(currentPawn);
            CheckAlliesOnTheWay(currentPawn);



            if (!ProcessValidCells(i, j, pawn.VallidCells))
            {
                return Errors.InvalidSquare;
            }

            currentPawn.isFirstMove = false;


            return Errors.NoErrors;
        }

    }

    class Bishop : Queen
    {


        public Bishop(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {


        }








        public override Errors ValidateSquares(ChessPiece bishop, int i, int j)
        {


            ProduceValidCells(bishop);



            if (ProcessValidCells(i, j, bishop.VallidCells) != true)
            {
                return Errors.InvalidSquare;
            }

            return Errors.NoErrors;

        }

        public override void ProduceValidCells(ChessPiece bishop)
        {
            bishop.VallidCells.Clear();
            Queen currentBishop = (Queen)bishop;
            ProduceDiagonalCells((currentBishop));
            CheckAlliesOnDiagonalyLines((currentBishop));
            MergeVallidCellsArray(currentBishop);
            DeleteInvalidCellsFromList(bishop);
        }
    }

    class Queen : ChessPiece
    {
        protected List<ChessCells> diagonalUpperLeftValidCells = new List<ChessCells>();
        protected List<ChessCells> diagonalUpperRightValidCells = new List<ChessCells>();
        protected List<ChessCells> diagonalLowerLeftValidCells = new List<ChessCells>();
        protected List<ChessCells> diagonalLowerRightValidCells = new List<ChessCells>();

        protected List<ChessCells> straightValidCells = new List<ChessCells>();

        protected List<ChessCells> straightUpLineValidCells = new List<ChessCells>();
        protected List<ChessCells> straightDownLineValidCells = new List<ChessCells>();
        protected List<ChessCells> straightLeftLineValidCells = new List<ChessCells>();
        protected List<ChessCells> straightRightLineValidCells = new List<ChessCells>();

        public Queen(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }


        protected static void ProduceDiagonalCells(Queen chessPiece)
        {
            chessPiece.diagonalLowerLeftValidCells.Clear();
            chessPiece.diagonalLowerRightValidCells.Clear();
            chessPiece.diagonalUpperLeftValidCells.Clear();
            chessPiece.diagonalUpperRightValidCells.Clear();




            int currentI = chessPiece.I;
            int currentJ = chessPiece.J;

            //Нижняя правая диагональ

            for (int column = chessPiece.I + 1; column < ChessTable.ChessCells.GetLength(0); column++)
            {
                for (int row = chessPiece.J + 1; row < ChessTable.ChessCells.GetLength(1); row++)
                {
                    if ((column - currentI == 1) && (row - currentJ == 1))
                    {

                        currentI = column;
                        currentJ = row;
                        //chessPiece.diagonalLowerRightValidCells.Add(currentI.ToString() + currentJ.ToString());
                        if ((currentI < 8 && currentI >= 0))
                        {
                            chessPiece.diagonalLowerRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));
                        }


                    }



                }

            }

            currentI = chessPiece.I;
            currentJ = chessPiece.J;

            //Нижняя левая диагональ

            for (int column = chessPiece.I + 1; column < ChessTable.ChessCells.GetLength(0); column++)
            {
                for (int row = chessPiece.J - 1; row >= 0; row--)
                {
                    if ((column - currentI == 1) && (row - currentJ == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        chessPiece.diagonalLowerLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

                    }

                }
            }

            currentI = chessPiece.I;
            currentJ = chessPiece.J;

            //Верхняя правая диагональ

            for (int column = chessPiece.I - 1; column >= 0; column--)
            {
                for (int row = chessPiece.J + 1; row < ChessTable.ChessCells.GetLength(1); row++)
                {
                    if ((currentI - column == 1) && (currentJ - row == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        chessPiece.diagonalUpperRightValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

                    }

                }
            }

            currentI = chessPiece.I;
            currentJ = chessPiece.J;

            //Верхняя левая диагональ

            for (int column = chessPiece.I - 1; column >= 0; column--)
            {
                for (int row = chessPiece.J - 1; row >= 0; row--)
                {

                    if ((currentI - column == 1) && (row - currentJ == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        chessPiece.diagonalUpperLeftValidCells.Add(ChessTable.GetChessCell(currentI, currentJ));

                    }




                }

            }



        }


        public static void MergeVallidCellsArray(Queen chessPiece)
        {
            chessPiece.VallidCells.Clear();
         

            var fields = chessPiece.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
           
            foreach (var member in fields)
            {
         

                var memberValue = member.GetValue(chessPiece);
                List<ChessCells> currentList = (List<ChessCells>)memberValue;

                chessPiece.VallidCells.AddRange(currentList);

            }

             

        }
        public static void MergeVallidCellsArray(Queen chessPiece, params List<ChessCells>[] Lists)
        {
            chessPiece.VallidCells.Clear();

            

                for (int i = 0; i < Lists.Length; i++)
                {
                    chessPiece.VallidCells.AddRange(Lists[i]);
                }

          


        }
       

        protected static void ProduceStraightCells(Queen chessPiece)
        {

            chessPiece.straightDownLineValidCells.Clear();
            chessPiece.straightUpLineValidCells.Clear();
            chessPiece.straightLeftLineValidCells.Clear();
            chessPiece.straightRightLineValidCells.Clear();



            if (chessPiece.I - 1 >=0)
            {
                for (int i = chessPiece.I - 1; i >= 0; i--)
                {
                    chessPiece.straightUpLineValidCells.Add(ChessTable.GetChessCell(i, chessPiece.J));
                }
            }

            if (chessPiece.I + 1 < ChessTable.ChessCells.GetLength(0))
            {
                for (int i = chessPiece.I + 1; i < ChessTable.ChessCells.GetLength(0); i++)
                {
                    chessPiece.straightDownLineValidCells.Add(ChessTable.GetChessCell(i, chessPiece.J));
                }
            }
         

            if (chessPiece.J - 1 >= 0)
            {
                for (int j = chessPiece.J - 1; j >= 0; j--)
                {
                    chessPiece.straightLeftLineValidCells.Add(ChessTable.GetChessCell(chessPiece.I, j));
                }

            }

            if (chessPiece.J + 1 < ChessTable.ChessCells.GetLength(1))
            {
                for (int j = chessPiece.J + 1; j < ChessTable.ChessCells.GetLength(1); j++)
                {
                    chessPiece.straightRightLineValidCells.Add(ChessTable.GetChessCell(chessPiece.I, j));
                }

            }




            //for (int i = 0; i < ChessTable.ChessCells.GetLength(0); i++)
            //{
            //    for (int j = 0; j < ChessTable.ChessCells.GetLength(1); j++)
            //    {
            //        if (chessPiece.I == i || chessPiece.J == j)

            //            chessPiece.straightValidCells.Add(i.ToString() + j.ToString());
            //        }

            //    }
            //}

        }


        public override void ProduceValidCells(ChessPiece queen)
        {
            queen.VallidCells.Clear();
            Queen currentQueen = (Queen)queen;
            ProduceDiagonalCells(currentQueen);
            ProduceStraightCells(currentQueen);
            CheckAlliesOnTheWay(currentQueen);
            MergeVallidCellsArray(currentQueen);
            DeleteInvalidCellsFromList(currentQueen);
           
        }
        


        public override Errors ValidateSquares(ChessPiece queen, int i, int j)
        {
            ProduceValidCells(queen);
      

            if (ProcessValidCells(i, j, queen.VallidCells) != true)
            {
                return Errors.InvalidSquare;
            }

         

            return Errors.NoErrors;
        }
        protected static void CheckAlliesOnDiagonalyLines(Queen chessPiece)
        {

            ComparerI comparerI = new ComparerI();
          
            List<ChessCells> tempolarCells = new List<ChessCells>(chessPiece.diagonalUpperLeftValidCells);

            int counter = 0;

            tempolarCells.Sort(comparerI);
            chessPiece.diagonalUpperLeftValidCells.Sort(comparerI);

            if (chessPiece.IsWhite)
            {




                foreach (var cell in tempolarCells)
                {



                    if (chessPiece.diagonalUpperLeftValidCells.Count >= counter)
                    {

                        if (cell.HasPiece && cell.ChessPiece.IsWhite)
                        {
                            chessPiece.diagonalUpperLeftValidCells.RemoveRange(counter, chessPiece.diagonalUpperLeftValidCells.Count - counter);
                            counter++;
                        }
                    }
                    else break;
                    counter++;


                }


             

                tempolarCells = new List<ChessCells>(chessPiece.diagonalUpperRightValidCells);

                tempolarCells.Sort(comparerI);
                chessPiece.diagonalUpperRightValidCells.Sort(comparerI);

                counter = 0;

                foreach (var cell in tempolarCells)
                {




                    if (chessPiece.diagonalUpperRightValidCells.Count >= counter)
                    {
                        if (cell.HasPiece && cell.ChessPiece.IsWhite)
                        {
                            chessPiece.diagonalUpperRightValidCells.RemoveRange(counter, chessPiece.diagonalUpperRightValidCells.Count - counter);
                            counter++;
                        }
                    }
                    else break;

                    counter++;
                }

                tempolarCells = new List<ChessCells>(chessPiece.diagonalLowerLeftValidCells);

           
                tempolarCells.Sort((x, y) => x.I.CompareTo(y.I));
                chessPiece.diagonalLowerLeftValidCells.Sort((x, y) => x.I.CompareTo(y.I));


                counter = 0;

                foreach (var cell in tempolarCells)
                {




                    if (chessPiece.diagonalLowerLeftValidCells.Count >= counter)
                    {
                        if (cell.HasPiece && cell.ChessPiece.IsWhite)
                        {
                            chessPiece.diagonalLowerLeftValidCells.RemoveRange(counter, chessPiece.diagonalLowerLeftValidCells.Count - counter);
                            
                        }
                    }
                    else break;
                    counter++;
                }
                 
                

            }

            tempolarCells = new List<ChessCells>(chessPiece.diagonalLowerRightValidCells);

            tempolarCells.Sort((x, y) => x.I.CompareTo(y.I));
            chessPiece.diagonalLowerRightValidCells.Sort((x, y) => x.I.CompareTo(y.I));
            counter = 0;

            foreach (var cell in tempolarCells)
            {




                if (chessPiece.diagonalLowerRightValidCells.Count >= counter)
                {
                    if (cell.HasPiece && cell.ChessPiece.IsWhite)
                    {
                        chessPiece.diagonalLowerRightValidCells.RemoveRange(counter, chessPiece.diagonalLowerRightValidCells.Count - counter);
                        
                    }
                }
                else break;
                counter++;



            }

        }
        protected static void CheckAlliesOnStraightigntLines(Queen chessPiece)
        {
        
            ComparerI comparerI = new ComparerI();
            ComparerJ comparerJ = new ComparerJ();
            List<ChessCells> tempolarCells = new List<ChessCells>(chessPiece.straightUpLineValidCells);

            int counter = 0;

            if (chessPiece.IsWhite)
            {
                tempolarCells.Sort(comparerI);
                chessPiece.straightUpLineValidCells.Sort(comparerI);

                foreach (var cell in tempolarCells)
                {




                    if (chessPiece.straightUpLineValidCells.Count >= counter)
                    {

                        if (cell.HasPiece && cell.ChessPiece.IsWhite)
                        {
                          
                            chessPiece.straightUpLineValidCells.RemoveRange(counter, chessPiece.straightUpLineValidCells.Count - counter);
                           
                        }

                    }
                    else break;
                    counter++;

                }

                tempolarCells = new  List<ChessCells>(chessPiece.straightRightLineValidCells);

                tempolarCells.Sort((x, y) => x.J.CompareTo(y.J));
                chessPiece.straightRightLineValidCells.Sort((x, y) => x.J.CompareTo(y.J));

               counter = 0;

                foreach (var cell in tempolarCells)
                {

                

              
                      


                        if (chessPiece.straightRightLineValidCells.Count >= counter)
                        {
                            if (cell.HasPiece && cell.ChessPiece.IsWhite)
                            {
                                chessPiece.straightRightLineValidCells.RemoveRange(counter, chessPiece.straightRightLineValidCells.Count - counter);
                       
                            }


                        }
                        else;

                    counter++;
                }

                tempolarCells = new List<ChessCells>(chessPiece.straightLeftLineValidCells);

                tempolarCells.Sort(comparerJ);
                chessPiece.straightLeftLineValidCells.Sort(comparerJ);


                counter = 0;

                foreach (var cell in tempolarCells)
                {





                    if (chessPiece.straightLeftLineValidCells.Count >= counter)
                    {
                        if (cell.HasPiece && cell.ChessPiece.IsWhite)
                        {
                         
                            chessPiece.straightLeftLineValidCells.RemoveRange(counter, chessPiece.straightLeftLineValidCells.Count - counter);
                           

                        }
                    }
                    else break;
                    counter++;

                }

            }

            tempolarCells = new List<ChessCells>(chessPiece.straightDownLineValidCells);
            tempolarCells.Sort((x, y) => x.I.CompareTo(y.I));
            chessPiece.straightDownLineValidCells.Sort((x, y) => x.I.CompareTo(y.I));

            counter = 0;

            foreach (var cell in tempolarCells)
            {





                if (chessPiece.straightDownLineValidCells.Count >= counter)
                {
                    if (cell.HasPiece && cell.ChessPiece.IsWhite)
                    {
                 
                        chessPiece.straightDownLineValidCells.RemoveRange(counter, chessPiece.straightDownLineValidCells.Count - counter);
                       
                    }
                }
                else break;
                counter++;
            }



        }

        public override void CheckAlliesOnTheWay(ChessPiece chessPiece)
        {
            CheckAlliesOnDiagonalyLines((Queen)chessPiece);
            CheckAlliesOnStraightigntLines((Queen)chessPiece);

        }
    }


    class Rook : Queen
    {
       
        public Rook(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override  void ProduceValidCells(ChessPiece rook)
        {
            rook.VallidCells.Clear();
            Queen currentRook = (Queen)rook;
            ProduceStraightCells(currentRook);
            CheckAlliesOnStraightigntLines(currentRook);
            MergeVallidCellsArray(currentRook);
            DeleteInvalidCellsFromList(currentRook);
        }

        public override Errors ValidateSquares(ChessPiece rook, int i, int j)
        {
             rook.ProduceValidCells(rook);
 

            if (!ProcessValidCells(i,j,rook.VallidCells))
            {
                return Errors.InvalidSquare;
            }


            return Errors.NoErrors;
        }
    }

    class Knight : ChessPiece
    {
        enum KnightMoves
        {
            UpperLeftTopCell,
            UpperLeftBottomCell,
            UpperRightTopCell,
            UpperRightBottomCell,
            LowerLeftTopCell,
            LowerLeftBottomCell,
            LowerRightTopCell,
            LowerRightBottomCell
        }
        public Knight(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void CheckAlliesOnTheWay(ChessPiece chessPiece)
        {
            List<ChessCells> tempolarList = new List<ChessCells>(chessPiece.VallidCells);
            foreach (var cell in tempolarList )
            {
                if (chessPiece.IsWhite)
                {
                    if (cell.HasPiece && cell.ChessPiece.IsWhite)
                    {
                        chessPiece.VallidCells.Remove(cell);
                    }
                }
                

            }
        }
        private static void CheckKnightMovesValidation(Knight knight,KnightMoves knightMoves)
        {
            switch (knightMoves)
            {
                case KnightMoves.UpperLeftTopCell:

                    if ((knight.I - 2 >= 0 && knight.J - 1 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 2, knight.J - 1));
                    }

                    break;

                case KnightMoves.UpperLeftBottomCell:
                    if ((knight.I - 1 >= 0 && knight.J - 2 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J - 2));
                    }
                    break;

                case KnightMoves.LowerLeftBottomCell:
                    if ((knight.I + 1 <= 7 && knight.J - 2 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 1, knight.J - 2));
                    }
                    break;
                case KnightMoves.LowerLeftTopCell:
                    if ((knight.I + 2 <= 7 && knight.J - 1 >= 0))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 2, knight.J - 1));
                    }

                    break;
                case KnightMoves.UpperRightTopCell:
                    if ((knight.I - 2 >+ 0 && knight.J + 1 <= 7))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 2, knight.J + 1));
                    }
                    break;
                case KnightMoves.UpperRightBottomCell:
                    if ((knight.I - 1 >= 0 && knight.J + 2 <= 7))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J + 2));
                    }
                    break;
                case KnightMoves.LowerRightTopCell:
                    if ((knight.I + 2 <= 7 && knight.J + 1 <= 7))
                    {
                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 2, knight.J + 1));
                    }  

                    break;
                case KnightMoves.LowerRightBottomCell:
                    if ((knight.I + 1 <= 7 && knight.J + 2 <= 7))
                    {

                        knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 1, knight.J + 2));

                    }
                    break;
                default:
                    break;
            }
        }
        public override void ProduceValidCells(ChessPiece knight)
        {
            knight.VallidCells.Clear();

            //knight.VallidCells.Add((knight.I - 1).ToString() + (knight.J - 2).ToString());
            //knight.VallidCells.Add((knight.I - 2).ToString() + (knight.J - 1).ToString());
            //knight.VallidCells.Add((knight.I + 1).ToString() + (knight.J - 2).ToString());
            //knight.VallidCells.Add((knight.I + 2).ToString() + (knight.J - 1).ToString());


            //knight.VallidCells.Add((knight.I - 2).ToString() + (knight.J + 1).ToString());
            //knight.VallidCells.Add((knight.I - 1).ToString() + (knight.J + 2).ToString());
            //knight.VallidCells.Add((knight.I + 2).ToString() + (knight.J + 1).ToString());
            //knight.VallidCells.Add((knight.I + 1).ToString() + (knight.J + 2).ToString());

            //chessPiece.validCells.RemoveAll((cell => !Regex.IsMatch((cell.I.ToString() + cell.J.ToString()), "(?=.{2}$)[0-7][0-7]+")));
          
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J - 2));
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 2, knight.J - 1));
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 1, knight.J - 2));
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 2, knight.J - 1));


            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 2, knight.J + 1));
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J + 2));
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I - 1, knight.J + 1));
            //knight.VallidCells.Add(ChessTable.GetChessCell(knight.I + 1, knight.J + 2));

            for (int i = 0; i <= 8; i++)
            {
                CheckKnightMovesValidation((Knight)knight, (KnightMoves)i);
            }
            CheckAlliesOnTheWay(knight);



            DeleteInvalidCellsFromList(knight);
        }

        public override Errors ValidateSquares(ChessPiece knight, int i, int j)
        {
            ProduceValidCells(knight);
         
            if (!ProcessValidCells(i,j,knight.VallidCells))
            {
                return Errors.InvalidSquare;
            }
            
            return Errors.NoErrors;


        }
    }

    class King : ChessPiece
    {
   
        public King(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void CheckAlliesOnTheWay(ChessPiece chessPiece)
        {
           
        }

        public override void ProduceValidCells(ChessPiece king)
        {
            King currentKing = (King)king;
            currentKing.validCells.Clear();

            for (int i = currentKing.I - 1; i <= currentKing.I + 1; i++)
            {
                for (int j = currentKing.J - 1; j <= currentKing.J + 1; j++)
                {
                    if ((i < 8 && i >= 0) && (j < 8 && j >= 0))
                    {
                        //currentKing.validCells.Add(i.ToString() + j.ToString());
                        currentKing.validCells.Add(ChessTable.GetChessCell(i,j));
                    }


                }
            }
            DeleteInvalidCellsFromList(currentKing);
        }

        public override Errors ValidateSquares(ChessPiece king, int i, int j)
        {
            King currentKing = (King)king;

            ProduceValidCells(currentKing);


            if (!ProcessValidCells(i,j,currentKing.validCells))
            {
                return Errors.InvalidSquare;
            }
            else

                return Errors.NoErrors;

        }
    }


}
