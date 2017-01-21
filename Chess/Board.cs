using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    public class Board
    {
        public Label L;
        public Panel P;
        public Color MyColor;
        public int color;
        public Pieces piece { get; set; }
        public PicesBase PB;
    }

    public enum Pieces { Pawn, Knight, Bishop, Rook, Queen, King,Empty };
    public enum PieceColor { White,Black};
       
    public class Moves
    {
        public int num;
        public int[] i;
        public int[] j;
    }

    
}
