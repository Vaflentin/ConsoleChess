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

        public override void CheckPiecesOnTheWay(ChessPiece chessPiece)
        {
            List<ChessCells> tempolarCells = new List<ChessCells>(chessPiece.VallidCells);
            foreach (var cell in tempolarCells)
            {
                if (cell.HasPiece)
                {
                    if (chessPiece.IsWhite && cell.ChessPiece.IsWhite)
                    {
                        chessPiece.VallidCells.Remove(cell);
                    }

                  if (!chessPiece.IsWhite && !cell.ChessPiece.IsWhite)
                    {
                        chessPiece.VallidCells.Remove(cell);
                    }
                }
            }
          
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
           
                        currentKing.validCells.Add(ChessTable.GetChessCell(i, j));
                    }


                }
            }
            DeleteInvalidCellsFromList(currentKing);
        }

    }
}
