using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Rest
{
    [ServiceContract]
    public interface IChessService
    {
        [OperationContract]
        [WebGet]
        string GetChessboard();
        [OperationContract]
        [WebGet]
        string GetPossibleMoves();
        [OperationContract]
        [WebInvoke]
        string PostMove(string inputMessage);
    }
}
