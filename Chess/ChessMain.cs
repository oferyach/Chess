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
                    BoardP[i, j].piece = (int)Pieces.Empty;
                }
            //now fill
            BoardP[0, 0].L.Text = "♜"; BoardP[0, 0].color = (int)PieceColor.Black; BoardP[0, 0].piece = (int)Pieces.Rook; 
            BoardP[1, 0].L.Text = "♞"; BoardP[1, 0].color = (int)PieceColor.Black; BoardP[1, 0].piece = (int)Pieces.Knight;
            BoardP[2, 0].L.Text = "♝"; BoardP[2, 0].color = (int)PieceColor.Black; BoardP[2, 0].piece = (int)Pieces.Bishop;
            BoardP[3, 0].L.Text = "♛"; BoardP[3, 0].color = (int)PieceColor.Black; BoardP[3, 0].piece = (int)Pieces.Queen;
            BoardP[4, 0].L.Text = "♚"; BoardP[4, 0].color = (int)PieceColor.Black; BoardP[4, 0].piece = (int)Pieces.King;
            BoardP[5, 0].L.Text = "♝"; BoardP[5, 0].color = (int)PieceColor.Black; BoardP[5, 0].piece = (int)Pieces.Bishop;
            BoardP[6, 0].L.Text = "♞"; BoardP[6, 0].color = (int)PieceColor.Black; BoardP[6, 0].piece = (int)Pieces.Knight;
            BoardP[7, 0].L.Text = "♜"; BoardP[7, 0].color = (int)PieceColor.Black; BoardP[7, 0].piece = (int)Pieces.Rook;
            for (int k = 0; k < 8; k++)
            {
                BoardP[k, 1].L.Text = "♟"; BoardP[k, 1].color = (int)PieceColor.Black; BoardP[k, 1].piece = (int)Pieces.Pawn;
            }
            BoardP[0, 7].L.Text = "♖"; BoardP[0, 7].color = (int)PieceColor.White; BoardP[0, 7].piece = (int)Pieces.Rook;
            BoardP[1, 7].L.Text = "♘"; BoardP[1, 7].color = (int)PieceColor.White; BoardP[1, 7].piece = (int)Pieces.Knight;
            BoardP[2, 7].L.Text = "♗"; BoardP[2, 7].color = (int)PieceColor.White; BoardP[2, 7].piece = (int)Pieces.Bishop;
            BoardP[3, 7].L.Text = "♕"; BoardP[3, 7].color = (int)PieceColor.White; BoardP[3, 7].piece = (int)Pieces.Queen;
            BoardP[4, 7].L.Text = "♔"; BoardP[4, 7].color = (int)PieceColor.White; BoardP[4, 7].piece = (int)Pieces.King;
            BoardP[5, 7].L.Text = "♗"; BoardP[5, 7].color = (int)PieceColor.White; BoardP[5, 7].piece = (int)Pieces.Bishop;
            BoardP[6, 7].L.Text = "♘"; BoardP[6, 7].color = (int)PieceColor.White; BoardP[6, 7].piece = (int)Pieces.Knight;
            BoardP[7, 7].L.Text = "♖"; BoardP[7, 7].color = (int)PieceColor.White; BoardP[7, 7].piece = (int)Pieces.Rook;
            for (int l = 0; l < 8; l++)
            {
                BoardP[l, 6].L.Text = "♙"; BoardP[l, 6].color = (int)PieceColor.White; BoardP[l, 6].piece = (int)Pieces.Pawn;
            }

            if (LastMarki!=-1)
                BoardP[LastMarki, LastMarkj].P.BackColor = BoardP[LastMarki, LastMarkj].MyColor;


            LastMarki = -1;
            TurnOf = (int)PieceColor.White;  // White allways starts
            SelectionMade = false;
            
        }


        public void MarkValidMoves(Moves moves)
        {
            if (moves == null)
                return;
            for (int i=0;i< moves.num;i++)
            {
                BoardP[moves.i[i], moves.j[i]].P.BackColor = Color.SkyBlue;
            }
        }

        Moves LastValidMoves = null;

        public bool IsValidMove(int i, int j)
        {
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
                    SelectionMade = false;
                    
                    MsgList.Items.Insert(0,"Cancle selection:"+ LastMarki.ToString()+":"+LastMarkj.ToString());
                    LastMarki = -1;
                    return;
                }
                //we check for legal move
                //temp just move to empty cell
                if (IsValidMove(i,j) //(BoardP[i,j].piece == (int)Pieces.Empty)
                {
                    MsgList.Items.Insert(0,"Do move: "+LastMarki.ToString()+":"+LastMarkj.ToString()+"-->"+i.ToString()+":"+j.ToString());
                    DoMove(LastMarki, LastMarkj, i, j);
                    SelectionMade = false;
                    if (TurnOf == (int)PieceColor.White)
                    {
                        TurnOf = (int)PieceColor.Black;
                    }
                    else
                        TurnOf = (int)PieceColor.White;
                }
            }
            else
            {
                //selection shoudl be on non empty of correct color 
                if (BoardP[i,j].color != TurnOf || BoardP[i,j].piece == (int)Pieces.Empty)
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
                    Moves m = GetValidMoves(i, j);
                    MarkValidMoves(m);
                }

            }
           

        }

        public void DoMove(int iFrom,int jFrom, int iTo, int jTo)
        {
            //take piece from pne place and put in other
            BoardP[iTo, jTo].piece = BoardP[iFrom, jFrom].piece;
            BoardP[iTo, jTo].color = BoardP[iFrom, jFrom].color;
            BoardP[iTo, jTo].L.Text = BoardP[iFrom, jFrom].L.Text;

            //clear current pos
            BoardP[iFrom, jFrom].piece = (int)Pieces.Empty;
            BoardP[iFrom, jFrom].color = 0;
            BoardP[iFrom, jFrom].L.Text = "";
            BoardP[iFrom, jFrom].P.BackColor = BoardP[iFrom, jFrom].MyColor;
            

            LastMarki = -1;
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            
            StartPosistion();
        }


        public Moves GetValidMoves(int i,int j)
        {
            switch (BoardP[i,j].piece)
            {
                case (int)Pieces.Pawn:
                    return GetValidPawnMoves(i, j);
                    break;
            }

            return null;
        }

        public Moves GetValidPawnMoves(int i,int j)
        {
            Moves moves = new Moves();

            moves.num = 0;
            moves.i = new int[3];
            moves.j = new int[3];

            for (int ii = 0; ii < 3; ii++ )
            {
                moves.num++;
                moves.i[ii] = ii;
                moves.j[ii] = ii;  
            }

            return moves;

        }
    }
}
