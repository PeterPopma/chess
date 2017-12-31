using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.CustomControls;

namespace Chess.Chess
{
    class ChessPiece
    {
        public enum ChessPieceType { None, Pawn, Rook, Horse, Bishop, Queen, King };
        public enum ChessPieceColor { None, White, Black };

        ChessPieceType type;
        ChessPieceColor color;
        bool hasMoved;

        public bool IsWhite { get => Color.Equals(ChessPieceColor.White); }
        public bool IsBlack { get => Color.Equals(ChessPieceColor.Black); }
        public bool IsNone { get => Color.Equals(ChessPieceColor.None); }

        public string PieceName { get => Type.ToString(); }
        internal ChessPieceType Type { get => type; set => type = value; }
        internal ChessPieceColor Color { get => color; set => color = value; }
        public bool HasMoved { get => hasMoved; set => hasMoved = value; }

        public ChessPiece(ChessPieceType type = ChessPieceType.None, ChessPieceColor color = ChessPieceColor.None, bool hasMoved=false)
        {
            this.Type = type;
            this.Color = color;
            this.hasMoved = hasMoved;
        }

    }
}
