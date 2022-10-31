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
       public static void CheckIsKingUnderAttack()
        {
         

                CheckIsKingOnTheWay(ChessManager._whitePlayer);
                RemoveCellIntersection(ChessManager._whitePlayer);

                CheckIsKingOnTheWay(ChessManager._blackPlayer);
                RemoveCellIntersection(ChessManager._blackPlayer);
        
            
        }

     
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
                if (piece.AllEnemiesOnTheWay.Exists(piece => piece.GetType() == typeof(King)))
                {
                    if (player.IsWhite)
                    {
                        king = (King)ChessManager._blackPlayer._playerPieces.Find(king => king.GetType() == typeof(King));

                        if (!CheckIsAllyCoverKing(king, piece))
                        {
                            king.IsChecked = true;
                        }
                   
                    }
                    else 
                    {
                        king = (King)ChessManager._whitePlayer._playerPieces.Find(king => king.GetType() == typeof(King));

                        if (!CheckIsAllyCoverKing(king, piece))
                        {
                            king.IsChecked = true;
                        }
                      
                    }
                    
                    return true;
                }
            }
            return false;
        }
        private static bool CheckIsAllyCoverKing(King king, ChessPiece enemyPiece)
        {
            foreach (var allyPiece in king.ProtectedAllies)
            {
                var attackedPiece = enemyPiece.AttactedEnemies.Find(attackedPiece => attackedPiece == allyPiece);

                if (attackedPiece != null)
                {

                    ProcessMovmentOfProtectingPiece(allyPiece, enemyPiece);

                    return true;
                } 
            }

            return false;

        }
        //foreach (var attackedCell in enemyPiece.VallidCells)
        //{
        //    if (allyPiece.VallidCells.Count !=0)
        //    {
        //        var allyList = new List<ChessCells>(allyPiece.VallidCells);


        //        foreach (var cell in allyList)
        //        {


        //            //if (cell != attackedCell)
        //            //{
        //            //    allyPiece.VallidCells.Clear();
        //            //}
        //        }
        //    }

        //}

        private static void ProcessMovmentOfProtectingPiece(ChessPiece allyPiece, ChessPiece enemyPiece)
        {
            PieceNames piece = (PieceNames)Enum.Parse(typeof(PieceNames), allyPiece.PieceName);
           
            switch (allyPiece)
            {
                case Queen:
                    ProcessProtecingPieceVallidCells(allyPiece, enemyPiece);
                    break;

                case Pawn:
                    ProcessProtecingPieceVallidCells(allyPiece, enemyPiece);
                    break;
        
            }
            //VallidCells.Find();
        }
      

        private static void ProcessProtecingPieceVallidCells(ChessPiece allyPiece, ChessPiece enemyPiece)
        {
            var allEnemyMovmentLists = enemyPiece.GetAllListFields();
            var allAllyMovmentLists = allyPiece.GetAllListFields();

            DeleteNullList(allAllyMovmentLists);
            DeleteNullList(allEnemyMovmentLists);

       

            foreach (var enemyList in allEnemyMovmentLists)
            {
                var tempEnemyList = new List<ChessCells>(enemyList);
                RemoveCellWithPieceFromList(tempEnemyList)
    ;

                foreach (var allyList in allAllyMovmentLists)
                {
                   var tempAllyList = new List<ChessCells>(allyList);
                   RemoveCellWithPieceFromList(tempAllyList);



                    if (tempAllyList.SequenceEqual(tempEnemyList))
                    {
                        allyPiece.VallidCells.Clear();
                        allyPiece.VallidCells.AddRange(allyList);

                        return;
                    }
                    else
                    {
                        allyPiece.VallidCells.Clear();
                    }
                }
            }

        }

        public override void CheckPiecesOnTheWay() // todo:: рефакторить
        {
            List<ChessCells> tempolarCells = new List<ChessCells>(VallidCells);

            foreach (var cell in tempolarCells)
            {

                if (cell.HasPiece)
                {
                    if (IsWhite && cell.ChessPiece.IsWhite || !IsWhite && !cell.ChessPiece.IsWhite)
                    {
                        ProtectedAllies.Add(cell.ChessPiece);
                        VallidCells.Remove(cell);
                    }
                    if (IsWhite && !cell.ChessPiece.IsWhite || !IsWhite && cell.ChessPiece.IsWhite)
                    {
                        AttactedEnemies.Add(cell.ChessPiece);
                    }

                }
            }
        }

        public override void ProduceValidCells()
        {

            _kingAllCellToMove.Clear();
        

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
