using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class ChessManager
    {

        static void Main()
        {

            Player firstPlayer = new Player(true);
            Player secondPlayer = new Player(false);
            ChessTable chessTable = ChessTable.Initialize();

            ChessMenu.CreateMenuFrame();


        }
    }
}
