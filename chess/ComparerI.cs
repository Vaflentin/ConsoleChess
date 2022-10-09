using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class ComparerI : IComparer<ChessCells>
    {


        public int Compare(ChessCells chessCell, ChessCells secondChessCell)
        {



             if (chessCell.I < secondChessCell.I)
            {
                return 1;
            }
            else if (chessCell.I > secondChessCell.I)
            {
                return -1;
            }
            else return 0;
          

        }
    }
    public class ComparerJ : IComparer<ChessCells>
    {


        public int Compare(ChessCells chessCell, ChessCells secondChessCell)
        {



            if (chessCell.J < secondChessCell.J)
            {
                return 1;
            }
            else if (chessCell.J > secondChessCell.J)
            {
                return -1;
            }
            else return 0;


        }
    }


}
    //public class Comparer : IComparer<ChessCells>
    //{


    //    public int Compare(ChessCells chessCell, ChessCells secondChessCell)
    //    {



    //        if (chessCell.I < secondChessCell.I)
    //        {
    //            return 1;
    //        }
    //        else if (chessCell.I > secondChessCell.I)
    //        {
    //            return -1;
    //        }
    //        else return 0;


    //    }
    //}


