using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace chess
{
 
    
    public abstract class ChessPiece
    {
   
        protected List<string> validCells = new List<string>();

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
     

        public List<string> VallidCells
        {
            get { return validCells; }
            set { validCells = value; }
        }

        public bool IsWhite { get; private set; }

        private string _pieceName;
        public int I { get; protected set; }
        public int J { get; protected set; }

       public static void DeleteInvalidCellsFromList(ChessPiece chessPiece)
        {

           
          chessPiece.validCells.RemoveAll((cell => !Regex.IsMatch(cell, "(?=.{2}$)[0-7][0-7]+")));

          chessPiece.validCells.RemoveAll(cell => cell == (chessPiece.I.ToString() + chessPiece.J.ToString()));

        }
       protected static bool ProcessValidCells(int i, int j, List<string> diagonal)
        {
            string cellToMove = i.ToString() + j.ToString();
            foreach (var diagonalsCell in diagonal)
            {
                if (diagonalsCell == cellToMove)
                {
                    return true;
                }
            }
            return false;
        }

    
       public  void Move(ChessPiece chessPiece, int i, int j)
        {
          
            if (ValidateSquares(chessPiece, i, j) == Errors.NoErrors)
            {
               
                ChessPiece currentPiece = chessPiece;

                ChessTable.DeletePiece(chessPiece.I, chessPiece.J);

               
                currentPiece.I = i;
                currentPiece.J = j;

                ChessTable.PlacePiece(chessPiece);

                ChessTable.SetChessLog(chessPiece.PieceName, chessPiece.I, chessPiece.J);
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
            Comparer comparer = new Comparer();
            List<string> tempolarValidCell = new List<string>(chessPiece.VallidCells);
            int i, j;
            int counter = 0;
            if (pawn.IsWhite)
            {
                tempolarValidCell.Sort(comparer);
                pawn.VallidCells.Sort(comparer);
               
                foreach (var cell in tempolarValidCell)
                {
                   

                    i = Convert.ToInt32(cell[0].ToString());
                    j = Convert.ToInt32(cell[1].ToString());
                    ChessCells currentCell;
                    currentCell = ChessTable.GetChessCell(i,j);

                    
                    if (currentCell.HasPiece && currentCell.ChessPiece.IsWhite)
                    {
                        pawn.validCells.RemoveRange(counter, pawn.validCells.Count - counter);
                   
                    }
                 
                    counter++;
                }
            }
            if (!pawn.IsWhite)
            {
                tempolarValidCell.Sort();
                pawn.validCells.Sort();
                foreach (var cell in pawn.validCells)
                {

                    i = Convert.ToInt32(cell[0].ToString());
                    j = Convert.ToInt32(cell[1].ToString());
                    ChessCells currentCell;
                    currentCell = ChessTable.GetChessCell(i, j);
                    if (currentCell.HasPiece && currentCell.ChessPiece.IsWhite)
                    {
                        pawn.validCells.RemoveRange(counter, pawn.validCells.Count);
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
                    currentPawn.validCells.Add((currentPawn.I - 1).ToString() + currentPawn.J.ToString());
                   
                }
                else
                {
                    currentPawn.validCells.Add((currentPawn.I - 2).ToString() + currentPawn.J.ToString());
                    currentPawn.validCells.Add((currentPawn.I - 1).ToString() + currentPawn.J.ToString());
      
                }
            }
            else
            {
                if (!currentPawn.isFirstMove)
                {
                    currentPawn.validCells.Add((currentPawn.I + 1).ToString() + currentPawn.J.ToString());
                }
                else
                {
                    currentPawn.validCells.Add((currentPawn.I + 2).ToString() + currentPawn.J.ToString());
                    currentPawn.validCells.Add((currentPawn.I + 1).ToString() + currentPawn.J.ToString());

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
            ProduceDiagonalCells((Queen)bishop);
            DeleteInvalidCellsFromList(bishop);
        }
    }

    class Queen : ChessPiece 
    {
        protected List<string> diagonalUpperLeftValidCells = new List<string>();
        protected List<string> diagonalUpperRightValidCells = new List<string>();
        protected List<string> diagonalLowerLeftValidCells = new List<string>();
        protected List<string> diagonalLowerRightValidCells = new List<string>();

        protected List<string> straightValidCells = new List<string>();

        protected List<string> straightUpLineValidCells = new List<string>();
        protected List<string> straightDownLineValidCells = new List<string>();
        protected List<string> straightLeftLineValidCells = new List<string>();
        protected List<string> straightRightLineValidCells = new List<string>();

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

            for (int column = chessPiece.I + 1; column <= ChessTable.ChessCells.GetLength(0); column++)
            {
                for (int row = chessPiece.J + 1; row <= ChessTable.ChessCells.GetLength(1); row++)
                {
                    if ((column - currentI == 1) && (row - currentJ == 1))
                    {

                        currentI = column;
                        currentJ = row;
                        chessPiece.diagonalLowerRightValidCells.Add(currentI.ToString() + currentJ.ToString());

                    }



                }

            }

            currentI = chessPiece.I;
            currentJ = chessPiece.J;

            //Нижняя левая диагональ

            for (int column = chessPiece.I + 1; column <= ChessTable.ChessCells.GetLength(0); column++)
            {
                for (int row = chessPiece.J - 1; row >= 0; row--)
                {
                    if ((column - currentI == 1) && (row - currentJ == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        chessPiece.diagonalLowerLeftValidCells.Add(currentI.ToString() + currentJ.ToString());

                    }

                }
            }

            currentI = chessPiece.I;
            currentJ = chessPiece.J;

            //Верхняя правая диагональ

            for (int column = chessPiece.I - 1; column >= 0; column--)
            {
                for (int row = chessPiece.J + 1; row <= ChessTable.ChessCells.GetLength(1); row++)
                {
                    if ((currentI - column == 1) && (currentJ - row == -1))
                    {

                        currentI = column;
                        currentJ = row;
                        chessPiece.diagonalUpperRightValidCells.Add(currentI.ToString() + currentJ.ToString());

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
                        chessPiece.diagonalUpperLeftValidCells.Add(currentI.ToString() + currentJ.ToString());

                    }




                }

            }

            MergeVallidCellsArray(chessPiece, chessPiece.diagonalLowerLeftValidCells, chessPiece.diagonalLowerRightValidCells, chessPiece.diagonalUpperLeftValidCells, chessPiece.diagonalUpperRightValidCells);

        }


      public static void MergeVallidCellsArray(Queen chessPiece, params List<string>[] Lists )
        {
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
                    chessPiece.straightUpLineValidCells.Add(i.ToString() + chessPiece.J.ToString());
                }
            }

            if (chessPiece.I + 1 < ChessTable.ChessCells.GetLength(0))
            {
                for (int i = chessPiece.I + 1; i < ChessTable.ChessCells.GetLength(0); i++)
                {
                    chessPiece.straightDownLineValidCells.Add(i.ToString() + chessPiece.J.ToString());
                }
            }
         

            if (chessPiece.J - 1 >= 0)
            {
                for (int j = chessPiece.J - 1; j >= 0; j--)
                {
                    chessPiece.straightLeftLineValidCells.Add(chessPiece.I.ToString()+j.ToString() );
                }

            }

            if (chessPiece.J + 1 < ChessTable.ChessCells.GetLength(1))
            {
                for (int j = chessPiece.J + 1; j < ChessTable.ChessCells.GetLength(1); j++)
                {
                    chessPiece.straightRightLineValidCells.Add(chessPiece.I.ToString() + j.ToString());
                }

            }



            MergeVallidCellsArray(chessPiece, chessPiece.straightUpLineValidCells, chessPiece.straightDownLineValidCells, chessPiece.straightLeftLineValidCells, chessPiece.straightRightLineValidCells);

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
            ProduceDiagonalCells((Queen)queen);
            ProduceStraightCells((Queen)queen);
            DeleteInvalidCellsFromList(queen);

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

        public override void CheckAlliesOnTheWay(ChessPiece chessPiece)
        {
            throw new System.NotImplementedException();
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
            ProduceStraightCells((Queen)rook);
            DeleteInvalidCellsFromList(rook);
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
        public Knight(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void CheckAlliesOnTheWay(ChessPiece chessPiece)
        {
            throw new System.NotImplementedException();
        }

        public override void ProduceValidCells(ChessPiece knight)
        {
            knight.VallidCells.Clear();

            knight.VallidCells.Add((knight.I - 1).ToString() + (knight.J - 2).ToString());
            knight.VallidCells.Add((knight.I - 2).ToString() + (knight.J - 1).ToString());
            knight.VallidCells.Add((knight.I + 1).ToString() + (knight.J - 2).ToString());
            knight.VallidCells.Add((knight.I + 2).ToString() + (knight.J - 1).ToString());


            knight.VallidCells.Add((knight.I - 2).ToString() + (knight.J + 1).ToString());
            knight.VallidCells.Add((knight.I - 1).ToString() + (knight.J + 2).ToString());
            knight.VallidCells.Add((knight.I + 2).ToString() + (knight.J + 1).ToString());
            knight.VallidCells.Add((knight.I + 1).ToString() + (knight.J + 2).ToString());
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
            throw new System.NotImplementedException();
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
                        currentKing.validCells.Add(i.ToString() + j.ToString());
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
