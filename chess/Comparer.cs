using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Comparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int iX = Convert.ToInt32(x[0].ToString());
            int iY = Convert.ToInt32(y[0].ToString());
            if (iX == iY)
            {
                return 0;
            }
            else if (iX < iY)
            {
                return 1;
            }
            else if (iX > iY)
            {
                return -1;
            }
            return 0;
          

        }
    }
}
