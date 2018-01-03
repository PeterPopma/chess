using Chess.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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

        public XElement GetCurrentPlayer()
        {
            XElement root = new XElement("Player");
            XElement elColor = new XElement("Color", displayMonogame.ActivePlayer ? "white" : "black");
            root.Add(elColor);
            return root;
        }

        public string GetPossibleMoves()
        {
            return "Calling Get for you ";
        }
        public string PostMove(string inputMessage)
        {
            return "Calling Post for you " + inputMessage;
        }
    }
}
