using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest
{
    public class ChessService : IChessService
    {
        Display DisplayMonogame;

        public string GetChessboard()
        {
            return "Calling Get for you ";
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
