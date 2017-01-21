using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class XY
    {
        public int X;
        public int Y;
    }
    public class PicesBase
    {
        public XY[] navM = new XY[25];
        public Moves moves = new Moves();
        public PieceColor MyColor { get; set; }
        public virtual Moves GetAllowedMoves(Board [,]B, int i, int j) { return null; }

        public PicesBase()
        {
            moves.num = 0;
            //init all navigation matrix
            for (int i = 0; i < 25; i++)
            {
                navM[i]= new XY();
                navM[i].X = -100;
            }
        }
        public void BuildBasedAllowedMoves(Board [,]P,int i, int j)
        {
            int k = 0;
            moves.num = 0;
            while (true)
            {
                if (navM[k].X == -100)
                    return;
                try
                {
                    if (P[i + navM[k].Y, j + navM[k].X].piece == Pieces.Empty || P[i + navM[k].Y, j + navM[k].X].PB.MyColor != P[i,j].PB.MyColor)
                    {
                        moves.i[moves.num] = i + navM[k].Y;
                        moves.j[moves.num] = j + navM[k].X;
                        moves.num++;
                    }
                }
                catch
                {
                    //we are out of boundery so ignor this one
                }
                k++;
            }
        }
    }
}
