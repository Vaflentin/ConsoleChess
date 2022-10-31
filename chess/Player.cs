using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Player
    {
        private static List<ChessPiece> _allPieces = new List<ChessPiece>(32);
        public List<ChessPiece> _playerPieces = new List<ChessPiece>(16);
        public bool rightToMove;
       public bool IsWhite { get; set; }

        public Player(bool isWhite)
        {
            IsWhite = isWhite;
        }

        private static void ProducePlayerPieces(List<ChessPiece> list)
        {
            foreach (var piece in list)
            {
                piece.ClearAllPieceLists();
                piece.ProduceValidCells();
                piece.FillAllAttackedEnmies();
                piece.CheckPiecesOnTheWay();
            }
           King.CheckIsKingUnderAttack();
        }
        public static void ProduceAllPieces()
        {
            _allPieces.Clear();

            _allPieces.AddRange(ChessManager._blackPlayer._playerPieces);
            _allPieces.AddRange(ChessManager._whitePlayer._playerPieces);
            ProducePlayerPieces(_allPieces);
            //ProducePlayerPieces(ChessManager._whitePlayer);
        }

        private static void  GetAllPieces()
        {
            var chessTable = ChessTable.ChessCells;

            foreach (var cell in chessTable)
            {
 
                if (cell.HasPiece)
                {
                    var piece = cell.ChessPiece;

                    if (cell.ChessPiece.IsWhite)
                    {
                        AddPiece(ChessManager._whitePlayer, ref piece);
                    }
                    else AddPiece(ChessManager._blackPlayer, ref piece);
                }
            }
        }
       public static void AddPiece(Player player, ref ChessPiece piece)
        {
            if (player._playerPieces != null)
            {
                player._playerPieces.Add(piece);
            }
          
        }
        public static void SetFullChessBoard()

        {
            PlaceKings();
            PlaceBishops();
            PlaceKnigths();
            PlacePawns();
            PlaceQueens();
            PlaceRooks();

            GetAllPieces();
        }


        private static void PlaceRooks()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {

                    if (i == 0 && (j == 0 || j == 7))
                    {
                        Rook rook = new Rook(i, j, "r", false);
                        ChessTable.PlacePiece(rook);
                    }
                    if (i == 7 && (j == 0 || j == 7))
                    {
                        Rook rook = new Rook(i, j, "r", true);
                        ChessTable.PlacePiece(rook);
                    }

                }
            }
        }


        private static void PlacePawns()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {

                    if (i == 1)
                    {
                        Pawn pawn = new Pawn(i, j, "p", false);
                        ChessTable.PlacePiece(pawn);
                    }
                    if (i == 6)
                    {
                        Pawn pawn = new Pawn(i, j, "p", true);
                        ChessTable.PlacePiece(pawn);
                    }

                }
            }
        }


        private static void PlaceQueens()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {


                    if ((i == 0 || i == 7) && (j == 3))
                    {
                        if (i == 0)
                        {
                            Queen queen = new Queen(i, j, "q", false);
                            ChessTable.PlacePiece(queen);
                        }
                        else
                        {
                            Queen queen = new Queen(i, j, "q", true);
                            ChessTable.PlacePiece(queen);
                        }

                    }

                }
            }
        }

        private static void PlaceKnigths()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {


                    if (i == 0 && (j == 1 || j == 6))
                    {
                        Knight knight = new Knight(i, j, "n", false);
                        ChessTable.PlacePiece(knight);
                    }
                    if (i == 7 && (j == 1 || j == 6))
                    {
                        Knight knight = new Knight(i, j, "n", true);
                        ChessTable.PlacePiece(knight);
                    }

                }
            }
        }
        private static void PlaceBishops()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {

                    if (i == 0 && (j == 2 || j == 5))
                    {
                        Bishop bishop = new Bishop(i, j, "b", false);
                        ChessTable.PlacePiece(bishop);
                    }
                    if (i == 7 && (j == 2 || j == 5))
                    {
                        Bishop bishop = new Bishop(i, j, "b", true);
                        ChessTable.PlacePiece(bishop);
                    }

                }
            }
        }
        private static void PlaceKings()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {

                    if ((i == 0 || i == 7) && (j == 4))
                    {
                        if (i == 0)
                        {
                            King king = new King(i, j, "k", false);
                            ChessTable.PlacePiece(king);
                        }
                        else
                        {
                            King king = new King(i, j, "k", true);
                            ChessTable.PlacePiece(king);
                        }

                    }


                }
            }
        }



        public static void CreatePiece(int i, int j, PieceNames pieceName)
        {

            switch (pieceName)
            {
                case PieceNames.p:
                    {
                        ChessPiece currentChessPiece = new Pawn(i, (int)j, pieceName.ToString(), true);
                        ChessTable.PlacePiece(currentChessPiece);
                    }
                    break;

                case PieceNames.b:
                    {
                        ChessPiece currentChessPiece = new Bishop(i, (int)j, pieceName.ToString(), true);
                        ChessTable.PlacePiece(currentChessPiece);
                    }
                    break;

                case PieceNames.q:
                    {
                        ChessPiece currentChessPiece = new Queen(i, (int)j, pieceName.ToString(), true);
                        ChessTable.PlacePiece(currentChessPiece);
                    }
                    break;

                case PieceNames.h:
                    {
                        ChessPiece currentChessPiece = new Knight(i, (int)j, pieceName.ToString(), true);
                        ChessTable.PlacePiece(currentChessPiece);
                    }
                    break;

                case PieceNames.r:
                    {
                        ChessPiece currentChessPiece = new Rook(i, (int)j, pieceName.ToString(), true);
                        ChessTable.PlacePiece(currentChessPiece);
                    }
                    break;

                case PieceNames.k:
                    {
                        ChessPiece currentChessPiece = new King(i, (int)j, pieceName.ToString(), true);
                        ChessTable.PlacePiece(currentChessPiece);
                    }
                    break;

                default:


                    break;

            }

        }

    }
}
