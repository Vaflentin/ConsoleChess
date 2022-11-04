using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    interface ICanPinDiagonaly
    {
       static List<ChessPiece> _enemyUpperLeftDiagonal = new();
       static List<ChessPiece> _enemyUpperRightDiagonal = new();
       static List<ChessPiece> _enemyLowerRightDiagonal = new();
       static List<ChessPiece> _enemyLowerLeftDiagonal = new();
    }
}
