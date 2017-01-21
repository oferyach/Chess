using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class P_Pawn : PicesBase
    {
        public P_Pawn(PieceColor Color)
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
            moves.i = new int[4];
            moves.j = new int[4];
            moves.num = 0;
            //first build the auto list
            //BuildBasedAllowedMoves(B, i, j);
            //now we have special moves for king

            
            // a white pawn will move down myboard

            if (B[i, j].color == (int)PieceColor.White)
            {
                if (j == 0) //on last row no moves
                    return moves;
                // pawn will move 'up' if free
                if (B[i, j - 1].piece == Pieces.Empty)
                {
                    moves.i[moves.num] = i;
                    moves.j[moves.num] = j - 1;
                    moves.num++;
                    //up was free check if we are on second line and 4 line is free
                    if (j == 6 && B[i, j - 2].piece == Pieces.Empty)
                    {
                        moves.i[moves.num] = i;
                        moves.j[moves.num] = j - 2;
                        moves.num++;
                    }
                }
                //check if can eat on each side
                //can it eat to the left
                if (i > 0 && j < 7 && B[i - 1, j - 1].piece != Pieces.Empty && B[i - 1, j - 1].color != (int)PieceColor.White)
                {
                    moves.i[moves.num] = i - 1;
                    moves.j[moves.num] = j - 1;
                    moves.num++;
                }
                //can it eat to the rigth
                if (i < 7 && j < 7 && B[i + 1, j - 1].piece != Pieces.Empty && B[i + 1, j - 1].color != (int)PieceColor.White)
                {
                    moves.i[moves.num] = i + 1;
                    moves.j[moves.num] = j - 1;
                    moves.num++;
                }

            }
            else
            {
                if (j == 7)
                    return moves;
                // pawn will move 'up' if free
                if (B[i, j + 1].piece == Pieces.Empty)
                {
                    moves.i[moves.num] = i;
                    moves.j[moves.num] = j + 1;
                    moves.num++;
                    //up was free check if we are on second line and 4 line is free
                    if (j == 1 && B[i, j + 2].piece == Pieces.Empty)
                    {
                        moves.i[moves.num] = i;
                        moves.j[moves.num] = j + 2;
                        moves.num++;
                    }
                }
                //check if can eat on each side
                //can it eat to the left
                if (i < 7 && j < 7 && B[i + 1, j + 1].piece != Pieces.Empty && B[i + 1, j + 1].color != (int)PieceColor.Black)
                {
                    moves.i[moves.num] = i + 1;
                    moves.j[moves.num] = j + 1;
                    moves.num++;
                }
                //can it eat to the rigth
                if (i > 0 && j < 7 && B[i - 1, j + 1].piece != Pieces.Empty && B[i - 1, j + 1].color != (int)PieceColor.Black)
                {
                    moves.i[moves.num] = i - 1;
                    moves.j[moves.num] = j + 1;
                    moves.num++;
                }
            }


            return moves;      
        }


    }
}
