using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Rook : Queen
    {
        public Rook(int i, int j, string pieceName, bool isWhite) : base(i, j, pieceName, isWhite)
        {
        }

        public override void ProduceValidCells()
        {
            VallidCells.Clear();
            ProduceStraightCells();
            MergeVallidCellsArray();
 
        }

    }
}
