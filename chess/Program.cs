using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace chess
{
    public enum Errors 
    {
        NoErrors,
        InvalidFormat,
        ChessSquareAlreadyTaken
        

    }

    public enum Collumns : byte
    {
        a, b, c, d, e, f, g, h
    }

    class Program
    {
        
     

        static void Main()
        {

            ChessTable chessTable = ChessTable.Initialize();
           

            ChessOutPut.InitialMessage();




        }
    }

    class Player
    {
     
        private static bool isWhitePlayer = true;
        private static bool isBlackPlayer = false;
       

        public enum Pieces : byte
        {
             p,
             b,
             q,
             k,
             h,
             r
        }

        public bool IsWhitePlayer
        {
           get { return isWhitePlayer; }
           private  set { isWhitePlayer = value; }
        }

        public bool IsBlackPlayer
        {
            get { return isBlackPlayer; }
           private set { isBlackPlayer = value; }
        }




        public static void CreatePiece(string playersPiece)
        {
           
            Collumns variableEnum;
            var i = 0;
            var j = 1;
            var chessName = 2;
            string pieceColor;
            string[] chessPiece = new string[4];


            if (isWhitePlayer)
            {
                pieceColor = "White";
                chessPiece[3] = pieceColor;
            }
            else
            {
                pieceColor = "Black";
                chessPiece[3] = pieceColor;
            }

          
                  
                    chessPiece[i] = Convert.ToString(playersPiece[2]);

                    variableEnum = (Collumns)Enum.Parse(typeof(Collumns), Convert.ToString(playersPiece[1]).ToLower(), ignoreCase: true);

                    chessPiece[j] = Convert.ToString((int)variableEnum);
                    chessPiece[chessName] = Convert.ToString(playersPiece[0]);



                    MakePiece(chessPiece);

                

        }


        public static void MakePiece(string[] chessPiece)
        {
            int i = (Convert.ToInt32(chessPiece[0])-1);
            int j = Convert.ToInt32(chessPiece[1]);
            string pieceName = chessPiece[2].ToLower();
            string pieceColor = chessPiece[3];

            ChessPiece currentChessPiece = new ChessPiece(i, j, pieceName, pieceColor);

            ChessTable.PlacePiece(currentChessPiece);

        }
    }

    class ChessCells
    {
      
        private string _cellsCondition;
        private bool _hasPiece;
        private ChessPiece _chessPiece;
        private bool _cellIsWhite;
    
   
    

        public ChessCells(int i, int j)
        {
           

            if ((1 - i + j) % 2 == 0)
            {

                CellIsWhite = false;
                ColorOfCell = "   ";

            }
            else
            {

                CellIsWhite = true;
                ColorOfCell = "███";

            }

        }

        public void SetHasPiece(bool hasPiece)
        {
            HasPiece = hasPiece;
        }

        public void SetChessPiece(ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;
        }

        public ChessPiece ChessPiece
        {
            get 
            { 
                return _chessPiece; 
            }

           private set
            {
                _chessPiece = value; 
            }
        }

     
        public string CellsCondition
        {
            get 
            { 
                return _cellsCondition;
            }

             private  set 
            { 
                _cellsCondition = value; 
            }
        }

        public bool CellIsWhite
        {
            get { return _cellIsWhite; }
           private set { _cellIsWhite = value; }
        }

        public string ColorOfCell
        {
            get
            {
                return _cellsCondition;
            }
          private  set
            {
                _cellsCondition = value;
            }
        }


        public bool HasPiece
        {
            get
            {
                return _hasPiece;
            }

          private  set
            {
                _hasPiece = value;
            }
        }



        public static bool GetCellCondition(int i, int j)
        {
            var currentCell = ChessTable.GetChessCell(i, j);
            return currentCell.HasPiece;
        }


        public bool SetCellCondition(ChessCells chessCell)
        {

            if (chessCell.HasPiece == true)
            {
                chessCell.CellsCondition = chessCell.CellsCondition.Remove(1, 1).Insert(1, chessCell.ChessPiece.pieceName);
        
            }

            return HasPiece;            
        }
   


    }

    class ChessPiece 
    {
        private string PieceColor;
        private string _pieceName;
        public int I { get; private set; }
        public int J { get; private set; }

        public ChessPiece(int i, int j, string pieceName, string pieceColor)
        {

            {
                PieceColor = pieceColor;
                _pieceName = pieceName;
                I = i;
                J = j;
            }
          
        }



      

        public string pieceName
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


    class ChessTable
    {
        private static ChessTable single = null;
        private static ChessCells[,] chessCells = new ChessCells[8, 8];
        private static string[,] fakeChessTable = new string[10, 10];
        private static List<string> chessLogs = new();


  


        public static List<string> ChessLogs
        {
            get { return chessLogs; }
           private set { chessLogs = value; }
        }


      


        public static string[,] MakeFakeChessTable()
        {
          
            for (int i = 0; i < fakeChessTable.GetLength(0); i++)
            {
                for (int j = 0; j < fakeChessTable.GetLength(1); j++)
                {
                    if (((i < 1) || ((i > 8 ))) && ((j >= 1) && (j <= 8)))
                    {
                        
                        fakeChessTable[i, j] = ($"  {Convert.ToString((Collumns)(j - 1))}");


                    }

                        if (((i < 9)&&(i > 0))&&((j == 0) || (j == 9)))
                        {

                            fakeChessTable[i, j] = Convert.ToString($"{i}");
                        }


                    else
                    {
                        if (((i >= 1)&&(j >= 1))&&(i<=8) && (j<=8))
                        {
                            fakeChessTable[i, j] = chessCells[i - 1, j - 1].CellsCondition;

                        }
                       
                    }
                  
                }
              
            }
            return fakeChessTable;
        }


        public static void SetChessLog(string pieceName, int i, int j)
        {
          chessLogs.Add($"{pieceName}{(Collumns)j}{Convert.ToString(i + 1)}");
        }

        public static void PlacePiece(ChessPiece chessPiece)
        {
         
         
                chessCells[chessPiece.I, chessPiece.J].SetHasPiece(true);
                chessCells[chessPiece.I, chessPiece.J].SetChessPiece(chessPiece);
                chessCells[chessPiece.I, chessPiece.J].SetCellCondition(chessCells[chessPiece.I, chessPiece.J]);

                SetChessLog(chessPiece.pieceName, chessPiece.I, chessPiece.J);

           
            

          
        }

      public static ChessCells GetChessCell(int i, int j)
        {
           return chessCells[i, j];
        }


        protected ChessTable()
        {

        }


        public static ChessTable Initialize()
        {
            if (single == null)
            {

                single = new ChessTable();
                Console.OutputEncoding = System.Text.Encoding.UTF8;


                for (int i = 0; i < chessCells.GetLength(0); i++)
                {
                    for (int j = 0; j < chessCells.GetLength(1); j++)
                    {

                        chessCells[i, j] = new ChessCells(i, j);

                    }

                }



           
            }

            return single;
        }
    }
}


