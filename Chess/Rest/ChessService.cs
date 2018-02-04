using Chess.Chess;
using Chess.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Chess.Chess.ChessPiece;

namespace Chess
{
    public class ChessService : IChessService
    {
        Display displayMonogame;

        public ChessService(Display displayMonogame)
        {
            this.displayMonogame = displayMonogame;
        }

        public XElement GetChessboard()
        {
            XElement root = new XElement("Chessboard");

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    string color = "none";
                    if (displayMonogame.ChessBoard[x, y].IsWhite)
                    {
                        color = "white";
                    }
                    if (displayMonogame.ChessBoard[x, y].IsBlack)
                    {
                        color = "black";
                    }
                    XElement elChesspiece = new XElement("Chesspiece");
                    root.Add(elChesspiece);
                    XAttribute attribX = new XAttribute("x", x);
                    elChesspiece.Add(attribX);
                    XAttribute attribY = new XAttribute("y", y);
                    elChesspiece.Add(attribY);
                    XElement elColor = new XElement("Color", color);
                    elChesspiece.Add(elColor);
                    XElement elPieceType = new XElement("PieceType", displayMonogame.ChessBoard[x, y].Type.ToString());
                    elChesspiece.Add(elPieceType);
                }
            }

            return root;
        }


        // Returns gamestatus playing or finished
        public XElement GetGameStatus()
        {
            XElement root = new XElement("GameStatus");
            XAttribute attribColor, attribSatus;

            if (displayMonogame.PlayerWon.Equals(ChessPieceColor.None))
            {
                attribColor = new XAttribute("player", displayMonogame.ActivePlayer ? "white" : "black");
                attribSatus = new XAttribute("status", "playing");
            }
            else
            {
                attribColor = new XAttribute("player", displayMonogame.PlayerWon.Equals(ChessPieceColor.White)  ? "white" : "black");
                attribSatus = new XAttribute("status", "won");
            }

            root.Add(attribSatus);
            root.Add(attribColor);

            return root;
        }

        public XElement GetMoveablePositions()
        {
            List<ChessboardPosition> pieces = displayMonogame.GetMoveablePositions();
            XElement root = new XElement("MoveablePositions");
            foreach(ChessboardPosition position in pieces)
            {
                XElement elChesspiece = new XElement("Position");
                root.Add(elChesspiece);
                XAttribute attribX = new XAttribute("x", position.X);
                elChesspiece.Add(attribX);
                XAttribute attribY = new XAttribute("y", position.Y);
                elChesspiece.Add(attribY);
            }

            return root;
        }

        public XElement GetPossibleMoves(int x, int y)
        {
            List<ChessboardPosition> moves = displayMonogame.CheckPossibleMoves(x, y);
            XElement root = new XElement("PossibleMoves");
            foreach (ChessboardPosition position in moves)
            {
                XElement elChesspiece = new XElement("Position");
                root.Add(elChesspiece);
                XAttribute attribX = new XAttribute("x", position.X);
                elChesspiece.Add(attribX);
                XAttribute attribY = new XAttribute("y", position.Y);
                elChesspiece.Add(attribY);
            }

            return root;
        }

        public XElement Move(int xfrom, int yfrom, int xto, int yto)
        {
            XElement root = new XElement("Move");
            string result = displayMonogame.MakeMove(xfrom, yfrom, xto, yto);
            XAttribute attribResult = new XAttribute("result", result);
            root.Add(result);
            return root;
        }
    }
}
