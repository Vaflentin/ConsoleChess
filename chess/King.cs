using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class King : ChessPiece
    {
       private List<ChessCells> _kingAllCellToMove = new List<ChessCells>(8);
        private bool IsChecked { get; set; }
        public King(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }
        private void CheckIsKingUnderAttack()
        {
         

                CheckIsKingOnTheWay(ChessManager._whitePlayer);
                RemoveCellIntersection(ChessManager._whitePlayer);

                CheckIsKingOnTheWay(ChessManager._blackPlayer);
                RemoveCellIntersection(ChessManager._blackPlayer);
        
            
            //ProduceDiagonalCells();
            //ProduceStraightCells();

            //MergeVallidCellsArray(
            //    SuspiciousChecksCells,
            //    diagonalLowerLeftValidCells, 
            //    diagonalLowerRightValidCells, 
            //    diagonalUpperLeftValidCells,
            //    diagonalUpperRightValidCells,
            //    straightDownLineValidCells,  
            //    straightLeftLineValidCells, 
            //    straightRightLineValidCells, 
            //    straightUpLineValidCells
            //    );
            //SortThroughCellLists();
        }

        //protected override void SortThroughCellLists()
        //{
        //    var allListOfValidCells = GetAllListFields();

        //    foreach (var list in allListOfValidCells)
        //    {
        //        ProccessSuspiciousCells(list);
        //    }
        //}
       private static List<ChessCells> AssignPieceList(ChessPiece chessPiece)
        {
            List<ChessCells> attacingCellList;
            switch (chessPiece.PieceName)
            {
                case "k":

                    var currentKing = (King)chessPiece;
                    attacingCellList = currentKing._kingAllCellToMove;

                    break;

                case "p":

                    var pawn = (Pawn)chessPiece;
                    attacingCellList = pawn.AttackingCells;

                    break;

                default:
                    attacingCellList = chessPiece.VallidCells;
                    break;
            }
            return attacingCellList;
        }
        private static void RemoveCellIntersection(Player player)
        {
            List<ChessCells> attackingCells;
            King enemyKing;

            if (player.IsWhite)
            {
               enemyKing = (King)ChessManager._blackPlayer._playerPieces.Find(king => king.GetType() == typeof(King));
            }
            else enemyKing = (King)ChessManager._whitePlayer._playerPieces.Find(king => king.GetType() == typeof(King));

            foreach (var piece in player._playerPieces)
            {
                attackingCells = AssignPieceList(piece);

                foreach (var cell in attackingCells)
                {
                    if (enemyKing.VallidCells.Exists(kingCell => kingCell == cell))
                    {
                        enemyKing.VallidCells.Remove(cell);
                    }
                }
            }
        }
        private static bool CheckIsKingOnTheWay(Player player)
        {
            King king;

            foreach (var piece in player._playerPieces)
            {
                if (piece.AttactedEnemies.Exists(piece => piece.GetType() == typeof(King)))
                {
                    if (player.IsWhite)
                    {
                        king = (King)ChessManager._blackPlayer._playerPieces.Find(king => king.GetType() == typeof(King));

                        if (!king.IsWhite)
                        {
                            king.IsChecked = true;
                        }
                   
                    }
                    else 
                    {
                        king = (King)ChessManager._whitePlayer._playerPieces.Find(king => king.GetType() == typeof(King));
                        if (king.IsWhite)
                        {
                            king.IsChecked = true;
                        }
                      
                    }
                    
                    return true;
                }
            }
            return false;
        }


        private bool CheckAllyCoverKing(ChessPiece chessPiece)
        {
            int firstI, secondI;
            if (I < chessPiece.I)
            {
                firstI = I;
                secondI = chessPiece.I;
            }
            else
                firstI = chessPiece.I;
                secondI = I;


            foreach (var piece in chessPiece.AllEnemiesOnTheWay)
            {
                if (firstI < piece.I && piece.I < secondI)
                {
                    piece.VallidCells.Clear();
                    return true;
                }
            }
            return false;
            //foreach (var cell in kingsList)
            //{
            //    if (cell.HasPiece)
            //    {
            //        if (IsWhite && cell.ChessPiece.IsWhite)
            //        {
            //            return true;
            //        }
            //        else if (!IsWhite && !cell.ChessPiece.IsWhite)
            //        {
            //            return true;
            //        }
            //    }


            //}
            //return false;
        }

        //private void ProccessSuspiciousCells(List<ChessCells> currentList)
        //{
        //    ComparerI comparerI = new ComparerI();

        //    if (currentList == straightUpLineValidCells || currentList == straightLeftLineValidCells
        //        || currentList == diagonalUpperLeftValidCells || currentList == diagonalUpperRightValidCells)
        //    {
        //        currentList.Sort(comparerI);
        //    }

        //    foreach (var cell in currentList)
        //    {
        //        if (cell.HasPiece /*&& CheckIsKingOnTheWay()*/)
        //        {
            

        //            if (CheckAllyCoverKing(cell.ChessPiece))
        //            {
                
        //            }
        //            else
        //            {
        //                VallidCells.Clear();
        //            }
        //        }
        //    }

        //}
        private  void CheckCanPieceCoverTheKing(King king)
        {
            //VallidCells.Find();
        }

        public override void CheckPiecesOnTheWay() // todo:: рефакторить
        {
            List<ChessCells> tempolarCells = new List<ChessCells>(VallidCells);

            foreach (var cell in tempolarCells)
            {

                if (cell.HasPiece)
                {
                    if (IsWhite && cell.ChessPiece.IsWhite)
                    {
                        ProtectedAllies.Add(cell.ChessPiece);
                        VallidCells.Remove(cell);
                    }
                    else AttactedEnemies.Add(cell.ChessPiece);

                    if (!IsWhite && !cell.ChessPiece.IsWhite)
                    {
                        ProtectedAllies.Add(cell.ChessPiece);
                        VallidCells.Remove(cell);
                    }
                    else AttactedEnemies.Add(cell.ChessPiece);
                }
            }
            CheckIsKingUnderAttack();
        }

        public override void ProduceValidCells()
        {

            _kingAllCellToMove.Clear();
            _validCells.Clear();

            for (int i = I - 1; i <= I + 1; i++)
            {
                for (int j = J - 1; j <= J + 1; j++)
                {
                    if ((i < 8 && i >= 0) && (j < 8 && j >= 0))
                    {
           
                        _validCells.Add(ChessTable.GetChessCell(i, j));
                  
                    }


                }
            }

            _kingAllCellToMove.AddRange(VallidCells);
        }
    }
}
