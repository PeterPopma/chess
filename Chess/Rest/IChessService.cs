using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chess
{
    [ServiceContract]
    public interface IChessService
    {
        [OperationContract]
        [WebGet]
        XElement GetChessboard();
        [OperationContract]
        [WebGet]
        string GetPossibleMoves();
        [OperationContract]
        [WebGet]
        XElement GetCurrentPlayer();
        [OperationContract]
        [WebInvoke]
        string PostMove(string inputMessage);
    }
}
