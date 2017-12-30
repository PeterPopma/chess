using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chess
{
    class ChessboardPosition
    {
        int x;
        int y;


        public ChessboardPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public ChessboardPosition()
        {
            this.X = -1;
            this.Y = -1;
        }

        public bool IsNone
        {
            get => x == -1;
        }

        public void setNone()
        {
            x = -1;
        }

        public void CopyValuesFrom(ChessboardPosition pos)
        {
            x = pos.X;
            y = pos.Y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
