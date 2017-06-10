using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessMain : Form
    {
        Board[,] BoardP;

        int LastMarki=-1, LastMarkj=0;

        enum BoardState { WhiteMove, BlackMove };

        int TurnOf = (int)PieceColor.White;

        bool SelectionMade = false;

        //int GameState = (int)BoardState.WhiteMove;

        public ChessMain()
        {
            InitializeComponent();

            CreateBoard();
            StartPosistion();
        }

        public void CreateBoard()
        {
            BoardP = new Board[8, 8];
            int c = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                    c = 0;
                else
                    c = 1;
                for (int j = 0; j < 8; j++)
                {
                    BoardP[i, j] = new Board();
                    BoardP[i, j].P = new Panel();

                    

                    BoardP[i, j].P.Size = new System.Drawing.Size(new Point(100, 100));
                    BoardP[i, j].P.Location = new Point(i * 100, 0 + j * 100);
                    if ((j + c) % 2 == 1)
                        BoardP[i, j].P.BackColor = Color.BurlyWood;
                    else
                        BoardP[i, j].P.BackColor = Color.WhiteSmoke;

                    BoardP[i, j].MyColor = BoardP[i, j].P.BackColor; //save it to resume

                    BoardP[i, j].L = new Label();

                    BoardP[i, j].L.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BoardPanel_MouseClick);
                    BoardP[i, j].L.Cursor = Cursors.Hand;
                    BoardP[i, j].L.Name = i.ToString() + ":" + j.ToString();

                    BoardP[i, j].L.Size = new System.Drawing.Size(98, 75);
                    
                    BoardP[i, j].L.ForeColor = Color.Black;
                    BoardP[i, j].L.Location = new System.Drawing.Point(10, 10);
                    BoardP[i, j].L.Font = new Font("Ariel", 48);


                    BoardP[i, j].P.Controls.Add(BoardP[i, j].L);

                    BoardPanel.Controls.Add(BoardP[i, j].P);
                }
            }
        }
        public void StartPosistion()
        {
            //clear board
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    BoardP[i, j].L.Text = "";
                    BoardP[i, j].piece = Pieces.Empty;
                    BoardP[i, j].P.BackColor = BoardP[i, j].MyColor;
                    BoardP[i, j].PB = null;
                }
            //now fill
            BoardP[0, 0].L.Text = "♜"; BoardP[0, 0].color = (int)PieceColor.Black; BoardP[0, 0].piece = Pieces.Rook; BoardP[0, 0].PB = new P_Rook(PieceColor.Black);
            BoardP[1, 0].L.Text = "♞"; BoardP[1, 0].color = (int)PieceColor.Black; BoardP[1, 0].piece = Pieces.Knight; BoardP[1, 0].PB = new P_Knight(PieceColor.Black);
            BoardP[2, 0].L.Text = "♝"; BoardP[2, 0].color = (int)PieceColor.Black; BoardP[2, 0].piece = Pieces.Bishop; BoardP[2, 0].PB = new P_Bishop(PieceColor.Black);
            BoardP[3, 0].L.Text = "♛"; BoardP[3, 0].color = (int)PieceColor.Black; BoardP[3, 0].piece = Pieces.Queen; BoardP[3, 0].PB = new P_Queen(PieceColor.Black);
            BoardP[4, 0].L.Text = "♚"; BoardP[4, 0].color = (int)PieceColor.Black; BoardP[4, 0].piece = Pieces.King; BoardP[4, 0].PB = new P_King(PieceColor.Black);
            BoardP[5, 0].L.Text = "♝"; BoardP[5, 0].color = (int)PieceColor.Black; BoardP[5, 0].piece = Pieces.Bishop; BoardP[5, 0].PB = new P_Bishop(PieceColor.Black);
            BoardP[6, 0].L.Text = "♞"; BoardP[6, 0].color = (int)PieceColor.Black; BoardP[6, 0].piece = Pieces.Knight; BoardP[6, 0].PB = new P_Knight(PieceColor.Black);
            BoardP[7, 0].L.Text = "♜"; BoardP[7, 0].color = (int)PieceColor.Black; BoardP[7, 0].piece = Pieces.Rook; BoardP[7, 0].PB = new P_Rook(PieceColor.Black);
            for (int k = 0; k < 8; k++)
            {
                BoardP[k, 1].L.Text = "♟"; BoardP[k, 1].color = (int)PieceColor.Black; BoardP[k, 1].piece = Pieces.Pawn; BoardP[k, 1].PB = new P_Pawn(PieceColor.Black);
            }
            BoardP[0, 7].L.Text = "♖"; BoardP[0, 7].color = (int)PieceColor.White; BoardP[0, 7].piece = Pieces.Rook; BoardP[0, 7].PB = new P_Rook(PieceColor.White);
            BoardP[1, 7].L.Text = "♘"; BoardP[1, 7].color = (int)PieceColor.White; BoardP[1, 7].piece = Pieces.Knight; BoardP[1, 7].PB = new P_Knight(PieceColor.White);
            BoardP[2, 7].L.Text = "♗"; BoardP[2, 7].color = (int)PieceColor.White; BoardP[2, 7].piece = Pieces.Bishop; BoardP[2, 7].PB = new P_Bishop(PieceColor.White);
            BoardP[3, 7].L.Text = "♕"; BoardP[3, 7].color = (int)PieceColor.White; BoardP[3, 7].piece = Pieces.Queen; BoardP[3, 7].PB = new P_King(PieceColor.White);
            BoardP[4, 7].L.Text = "♔"; BoardP[4, 7].color = (int)PieceColor.White; BoardP[4, 7].piece = Pieces.King; BoardP[4, 7].PB = new P_Queen(PieceColor.White);
            BoardP[5, 7].L.Text = "♗"; BoardP[5, 7].color = (int)PieceColor.White; BoardP[5, 7].piece = Pieces.Bishop; BoardP[5, 7].PB = new P_Bishop(PieceColor.White);
            BoardP[6, 7].L.Text = "♘"; BoardP[6, 7].color = (int)PieceColor.White; BoardP[6, 7].piece = Pieces.Knight; BoardP[6, 7].PB = new P_Knight(PieceColor.White);
            BoardP[7, 7].L.Text = "♖"; BoardP[7, 7].color = (int)PieceColor.White; BoardP[7, 7].piece = Pieces.Rook; BoardP[7, 7].PB = new P_Rook(PieceColor.White);
            for (int l = 0; l < 8; l++)
            {
                BoardP[l, 6].L.Text = "♙"; BoardP[l, 6].color = (int)PieceColor.White; BoardP[l, 6].piece = Pieces.Pawn; BoardP[l, 6].PB = new P_Pawn(PieceColor.White);
            }

            if (LastMarki!=-1)
                BoardP[LastMarki, LastMarkj].P.BackColor = BoardP[LastMarki, LastMarkj].MyColor;


            LastMarki = -1;
            UpdateTurnOff ((int)PieceColor.White);  // White allways starts
            SelectionMade = false;

            MsgList.Items.Clear();
            
        }


        public void MarkValidMoves(Moves moves, bool set)
        {
            if (moves == null)
                return;
            for (int i=0;i< moves.num;i++)
            {
                if (set)
                    BoardP[moves.i[i], moves.j[i]].P.BackColor = Color.SkyBlue;
                else
                    BoardP[moves.i[i], moves.j[i]].P.BackColor = BoardP[moves.i[i], moves.j[i]].MyColor;
            }
        }

        Moves LastValidMoves = null;

        public bool IsValidMove(Moves m,int i, int j)
        {
            if (m == null)
                return false;
            for (int ii = 0; ii < m.num; ii++)
            {
                if (m.i[ii] == i && m.j[ii] == j)
                    return true;               
            }
            return false;
        }

        private void BoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //board was clicked
            int i, j = 0;

            //find where click was made
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] val = (sender as Label).Name.Split(delimiterChars);

            i = int.Parse(val[0]);
            j = int.Parse(val[1]);
           
            
            //we can be in two mode - no selection made or selection was made and user want to mode 
            if (SelectionMade)
            {
                //if the same cell selected return the previous box to original color - and cancle selection
                if (i == LastMarki && j == LastMarkj)
                {                  
                    BoardP[LastMarki, LastMarkj].P.BackColor = BoardP[LastMarki, LastMarkj].MyColor;
                    MarkValidMoves(LastValidMoves, false);
                    SelectionMade = false;
                    
                    MsgList.Items.Insert(0,"Cancle selection:"+ LastMarki.ToString()+":"+LastMarkj.ToString());
                    LastMarki = -1;
                    return;
                }
                //we check for legal move
                //
                if (IsValidMove(LastValidMoves,i,j)) 
                {
                    MsgList.Items.Insert(0,"Do move: "+LastMarki.ToString()+":"+LastMarkj.ToString()+"-->"+i.ToString()+":"+j.ToString());
                    DoMove(LastMarki, LastMarkj, i, j);
                    SelectionMade = false;
                    if (TurnOf == (int)PieceColor.White)
                    {
                        UpdateTurnOff((int)PieceColor.Black);
                    }
                    else
                        UpdateTurnOff((int)PieceColor.White);
                }
            }
            else
            {
                //selection shoudl be on non empty of correct color 
                if (BoardP[i,j].color != TurnOf || BoardP[i,j].piece == Pieces.Empty)
                {
                    MsgList.Items.Insert(0, "Not your turn or empty cell");
                    System.Console.Beep();
                    return;
                }
                else
                {
                    MsgList.Items.Insert(0, "Cell selected: " + i.ToString() + ":" + j.ToString());
                    SelectionMade = true;
                    BoardP[i, j].P.BackColor = Color.Silver;  //marked it as selected
                    //remember the selection
                    LastMarki = i;
                    LastMarkj = j;

                    //find valid moves for this selection and mark them
                    LastValidMoves = GetValidMoves(i, j);
                    if (LastValidMoves.num == 0)
                        MsgList.Items.Insert(0, "No valid move for selection");
                    MarkValidMoves(LastValidMoves,true);
                }

            }
           

        }

        public void DoMove(int iFrom,int jFrom, int iTo, int jTo)
        {

            //clear marked valid moves
            MarkValidMoves(LastValidMoves, false);


            //take piece from and place and put in other
            BoardP[iTo, jTo].piece = BoardP[iFrom, jFrom].piece;
            BoardP[iTo, jTo].color = BoardP[iFrom, jFrom].color;
            BoardP[iTo, jTo].L.Text = BoardP[iFrom, jFrom].L.Text;
            BoardP[iTo, jTo].PB = BoardP[iFrom, jFrom].PB;

            //clear current pos
            BoardP[iFrom, jFrom].piece = Pieces.Empty;
            BoardP[iFrom, jFrom].color = 0;
            BoardP[iFrom, jFrom].L.Text = "";
            BoardP[iFrom, jFrom].P.BackColor = BoardP[iFrom, jFrom].MyColor;
            BoardP[iFrom, jFrom].PB = null;
            

            LastMarki = -1;
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            
            StartPosistion();
        }


        public Moves GetValidMoves(int i,int j)
        {
            Moves moves = new Moves();

            moves.num = 0;
            moves = BoardP[i, j].PB.GetAllowedMoves(BoardP, i, j);
            /* h
            switch (BoardP[i,j].piece)
            {
                case Pieces.Pawn:
                    moves = BoardP[i, j].PB.GetAllowedMoves(BoardP, i, j);
                    //moves =  GetValidPawnMoves(i, j);
                    break; 
                case Pieces.Knight:
                    moves = GetValidKnightMoves(i, j);
                    break;
                case Pieces.Bishop:
                    moves = GetValidBishopMoves(i, j);
                    break;
                case Pieces.Rook:
                    moves = GetValidRookMoves(i, j);
                    break;
                case Pieces.King:
                    moves = GetValidKingMoves(i, j);
                    break;
                case Pieces.Queen:
                    moves = GetValidQueenMoves(i, j);
                    break;

            }
            */
            

            return moves;
        }

        public Moves GetValidPawnMoves(int i,int j)
        {
            Moves moves = new Moves();
            //the max moves pawn can have are 4
            moves.i = new int[4];
            moves.j = new int[4];

            // a white pawn will move down myboard
            
            if (BoardP[i,j].color == (int)PieceColor.White )
            {
                if (j == 0) //on last row no moves
                    return moves; 
                // pawn will move 'up' if free
                if (BoardP[i,j-1].piece == Pieces.Empty)
                {
                    moves.i[moves.num] = i;
                    moves.j[moves.num] = j -1;
                    moves.num++;
                    //up was free check if we are on second line and 4 line is free
                    if (j == 6 && BoardP[i,j-2].piece == Pieces.Empty)
                    {
                        moves.i[moves.num] = i;
                        moves.j[moves.num] = j - 2;
                        moves.num++;
                    }
                }
                //check if can eat on each side
                //can it eat to the left
                if (i > 0 && j < 7 && BoardP[i - 1, j - 1].piece != Pieces.Empty && BoardP[i -1, j - 1].color != (int)PieceColor.White)
                {
                    moves.i[moves.num] = i - 1;
                    moves.j[moves.num] = j - 1;
                    moves.num++;
                }
                //can it eat to the rigth
                if (i < 7 && j < 7 && BoardP[i + 1, j - 1].piece != Pieces.Empty && BoardP[i + 1, j - 1].color != (int)PieceColor.White)
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
                if (BoardP[i, j + 1].piece == Pieces.Empty)
                {
                    moves.i[moves.num] = i;
                    moves.j[moves.num] = j + 1;
                    moves.num++;
                    //up was free check if we are on second line and 4 line is free
                    if (j == 1 && BoardP[i, j + 2].piece == Pieces.Empty)
                    {
                        moves.i[moves.num] = i;
                        moves.j[moves.num] = j + 2;
                        moves.num++;
                    }
                }
                //check if can eat on each side
                //can it eat to the left
                if (i < 7 && j < 7 && BoardP[i + 1, j + 1].piece != Pieces.Empty && BoardP[i + 1, j + 1].color != (int)PieceColor.Black)
                {
                    moves.i[moves.num] = i + 1;
                    moves.j[moves.num] = j + 1;
                    moves.num++;
                }
                //can it eat to the rigth
                if (i > 0 && j < 7 && BoardP[i - 1, j + 1].piece != Pieces.Empty && BoardP[i - 1, j + 1].color != (int)PieceColor.Black)
                {
                    moves.i[moves.num] = i - 1;
                    moves.j[moves.num] = j + 1;
                    moves.num++;
                }
            }
            

            return moves;

        }


        public Moves GetValidKnightMoves(int i, int j)
        {
            Moves moves = new Moves();
            //the max moves knight can have are 8
            moves.i = new int[8];
            moves.j = new int[8];

            //no diffrence between colors but cannot move to same pices color
            //one up : two to the left and two to the rigth
            if (j > 0)
            {
                if (i > 1) //two to the left
                {
                  if (BoardP[j-1,i-2].piece == Pieces.Empty || 
                      (BoardP[j-1,i-2].piece != Pieces.Empty && BoardP[j-1,i-2].color != BoardP[i,j].color))
                  {
                      moves.i[moves.num] = i - 2;
                      moves.j[moves.num] = j - 1;
                      moves.num++;
                  }
                }
                if (i < 6) //two to the rigth
                {
                    if (BoardP[j - 1, i + 2].piece == Pieces.Empty ||
                      (BoardP[j - 1, i + 2].piece != Pieces.Empty && BoardP[j - 1, i + 2].color != BoardP[i, j].color))
                    {
                        moves.i[moves.num] = i + 2;
                        moves.j[moves.num] = j - 1;
                        moves.num++;
                    }
                }
            }
            //two up : one to the rigth and one to the left
            if (j > 1)
            {

            }
            if (BoardP[i, j].color == (int)PieceColor.White)
            {

            }
            else
            {

            }
            return moves;
        }


        public Moves GetValidKingMoves(int i, int j)
        {
            return BoardP[i, j].PB.GetAllowedMoves(BoardP, i, j);
        }

        public Moves GetValidBishopMoves(int i, int j)
        {
            Moves moves = new Moves();
            //the max moves pawn can have are 4
            moves.i = new int[4];
            moves.j = new int[4];

            // a white pawn will move down myboard

            if (BoardP[i, j].color == (int)PieceColor.White)
            {
            }
            else
            {

            }
            return moves;
        }



        public Moves GetValidQueenMoves(int i, int j)
        {
            Moves moves = new Moves();
            //the max moves pawn can have are 4
            moves.i = new int[4];
            moves.j = new int[4];

            // a white pawn will move down myboard

            if (BoardP[i, j].color == (int)PieceColor.White)
            {
            }
            else
            {

            }
            return moves;
        }

        public Moves GetValidRookMoves(int i, int j)
        {
            Moves moves = new Moves();
            //the max moves pawn can have are 4
            moves.i = new int[4];
            moves.j = new int[4];

            // a white pawn will move down myboard

            if (BoardP[i, j].color == (int)PieceColor.White)
            {
            }
            else
            {

            }
            return moves;
        }

        public void UpdateTurnOff(int t)
        {
            TurnOf = t;
            if (TurnOf == (int)PieceColor.White)
                MoveCol.BackColor = Color.White;
            else
                MoveCol.BackColor = Color.Black;
        }

    }
}
