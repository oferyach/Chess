using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class P_Queen : PicesBase
    {
        public P_Queen(PieceColor Color)
        {
            MyColor = Color;
            //build the navigation table
            navM[0].X = -1;
            navM[0].Y = -1;

            navM[1].X = -1;
            navM[1].Y = +0;

            navM[2].X = -1;
            navM[2].Y = +1;

            navM[3].X = +1;
            navM[3].Y = -1;

            navM[4].X = +1;
            navM[4].Y = +0;

            navM[5].X = +1;
            navM[5].Y = +1;

            navM[6].X = +0;
            navM[6].Y = +1;

            navM[7].X = +0;
            navM[7].Y = -1;



        }
        public override Moves GetAllowedMoves(Board[,] B, int i, int j)
        {
            moves.i = new int[8];
            moves.j = new int[8];
            //first build the auto list
            BuildBasedAllowedMoves(B, i, j);
            //now we have special moves for king
            return moves;
        }


    }
}
