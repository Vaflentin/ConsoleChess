using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class King : ChessPiece 
    {

        public King(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {

        }

        public override void CheckPiecesOnTheWay(ChessPiece chessPiece) // todo:: рефакторить
        {
            List<ChessCells> tempolarCells = new List<ChessCells>(chessPiece.VallidCells);

            foreach (var cell in tempolarCells)
            {

                if (cell.HasPiece)
                {
                    if (chessPiece.IsWhite && cell.ChessPiece.IsWhite)
                    {
                        chessPiece.ProtectedAllies.Add(cell.ChessPiece);
                        chessPiece.VallidCells.Remove(cell);
                    }
                    else chessPiece.AttactedEnemies.Add(cell.ChessPiece);

                    if (!chessPiece.IsWhite && !cell.ChessPiece.IsWhite)
                    {
                        chessPiece.ProtectedAllies.Add(cell.ChessPiece);
                        chessPiece.VallidCells.Remove(cell);
                    }
                    else chessPiece.AttactedEnemies.Add(cell.ChessPiece);
                }
            }
          
        }

        public override void ProduceValidCells(ChessPiece king)
        {
            King currentKing = (King)king;
            currentKing._validCells.Clear();

            for (int i = currentKing.I - 1; i <= currentKing.I + 1; i++)
            {
                for (int j = currentKing.J - 1; j <= currentKing.J + 1; j++)
                {
                    if ((i < 8 && i >= 0) && (j < 8 && j >= 0))
                    {
           
                        currentKing._validCells.Add(ChessTable.GetChessCell(i, j));
                    }


                }
            }
            DeleteInvalidCellsFromList(currentKing);
        }

    }
}
