using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
   public class King : ChessPiece
    {
        private List<ChessCells> _kingAllCellToMove = new List<ChessCells>(8);
        //private List<ChessPiece> _kingCoveringPieces = new List<ChessPiece>();

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

        private static void CheckIsKingOnTheWay(Player player)
        {
            foreach (var playerPiece in player._playerPieces)
            {
                if (!playerPiece.AttactedEnemies.Exists(piece => piece.GetType() == typeof(King)))
                {
                    var enemiesPiecesLists = playerPiece.GetAllPublicChessCellsLists();
                    DeleteNullList(enemiesPiecesLists);

                    if (enemiesPiecesLists != null)
                    {
                        foreach (var listWithEnemies in enemiesPiecesLists)
                        {
                            if (listWithEnemies.Exists(piece => piece.HasPiece && piece.ChessPiece.GetType() == typeof(King)))
                            {
                                var kingCell = listWithEnemies.Find(piece => piece.HasPiece && piece.ChessPiece.GetType() == typeof(King));
                                King king = (King)kingCell.ChessPiece;
                                CheckIsAllyCoverKing(king, playerPiece, listWithEnemies);

                            }
                        }
                    }
                 
                }
                else
                {
                    //var king = (King)playerPiece.AttactedEnemies.Find(piece => piece.GetType() == typeof(King));

                    //king.IsChecked == true;
                }
              
                
            }
        }
            private static void CheckIsAllyCoverKing(King king, ChessPiece enemy, List<ChessCells> enemyChessPieceList)
            {

                foreach (var ally in enemyChessPieceList)
                {
                    
                        if (enemy.AttactedEnemies.Exists(attackedPiece => attackedPiece == ally.ChessPiece) && ally.ChessPiece != king)
                        {
                            List<ChessPiece> enemyPieces = new(DeleteNoPieceCells(enemyChessPieceList));
                            var currentElIndex = enemyPieces.IndexOf(ally.ChessPiece);
                            var kingIndex = enemyPieces.FindIndex(piece => piece.GetType() == typeof(King));

                            if (currentElIndex + 1 == kingIndex)
                            {
                                ProcessMovmentOfProtectingPiece(ally.ChessPiece, enemy, enemyChessPieceList);
                            }
                          
                             break;
                        }
                   
                }
            }

        //private void CheckIsPieceTherePieceBehind()
        //{

        //}


        
        

        private static void ProcessMovmentOfProtectingPiece(ChessPiece allyPiece, ChessPiece enemyPiece, List<ChessCells> enemyChessPieceList)
        {
            PieceNames piece = (PieceNames)Enum.Parse(typeof(PieceNames), allyPiece.PieceName);

            switch (allyPiece)
            {
                case Queen:
                    ProcessProtecingPieceVallidCells(allyPiece, enemyPiece, enemyChessPieceList);
                    break;

                case Pawn:
                    ProcessProtecingPieceVallidCells(allyPiece, enemyPiece, enemyChessPieceList);
                    break;

            }
            //VallidCells.Find();
        }


        private static void ProcessProtecingPieceVallidCells(ChessPiece allyPiece, ChessPiece enemyPiece, List<ChessCells> enemyChessPieceList)
        {
            var allEnemyMovmentLists = enemyPiece.GetAllChessCellsListFields();
            var allAllyMovmentLists = allyPiece.GetAllChessCellsListFields();

            DeleteNullList(allAllyMovmentLists);
            DeleteNullList(allEnemyMovmentLists);



            foreach (var enemyVallidCellList in allEnemyMovmentLists)
            {
                var tempEnemyList = new List<ChessCells>(enemyVallidCellList);
                RemoveCellWithPieceFromList(tempEnemyList);
                  

                foreach (var allyList in allAllyMovmentLists)
                {
                    var tempAllyList = new List<ChessCells>(allyList);
                    RemoveCellWithPieceFromList(tempAllyList);

                    if (tempAllyList.SequenceEqual(tempEnemyList))
                    {
                        allyPiece.VallidCells.Clear();

                        var newVallidCells = new List<ChessCells>(enemyChessPieceList);
                        var kingCell = newVallidCells.Find(piece => piece.HasPiece && piece.ChessPiece.GetType() == typeof(King));
                        var kingIndex = newVallidCells.IndexOf(kingCell);

                        newVallidCells.RemoveRange(kingIndex, newVallidCells.Count - kingIndex);

                        newVallidCells.Add(ChessTable.GetChessCell(enemyPiece.I, enemyPiece.J));
                        newVallidCells.Remove(ChessTable.GetChessCell(allyPiece.I, allyPiece.J));

                        allyPiece.VallidCells.AddRange(newVallidCells);

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
        

        //protected override void SortThroughCellLists()
        //{
        //    var allListOfValidCells = GetAllListFields();

        //    allListOfValidCells.RemoveAll(list => list == _kingAllCellToMove);
        //    allListOfValidCells.RemoveAll(list => list == _validCells);

        //    DeleteNullList(allListOfValidCells);

        //    foreach (var list in allListOfValidCells) // все фигуры на путях
        //    {

        //        AddEnemiesAndAllies(this, list);

        //    }
        //}
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

            //ProduceDiagonalCells();
            //ProduceStraightCells();

            //SortThroughCellLists();
            
            //base.ProcessPiecesOnTheWay();
        }

        
    }
}