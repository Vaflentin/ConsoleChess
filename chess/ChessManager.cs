using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class ChessManager
    {
        public static Player _whitePlayer;
        public static Player _blackPlayer;
        public static ChessTable _chessTable;
        public static void Main()

        {
            _whitePlayer  = new Player(true);
            _blackPlayer = new Player(false);
            _chessTable = ChessTable.Initialize();
            ChessMenu.CreateMenuFrame();
        }
    }
}
