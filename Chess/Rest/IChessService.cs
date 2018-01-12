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
        XElement GetMoveablePositions();
        [OperationContract]
        [WebGet(UriTemplate = "GetPossibleMoves?x={x}&y={y}")]
        XElement GetPossibleMoves(int x, int y);
        [OperationContract]
        [WebGet]
        XElement GetCurrentPlayer();
        [OperationContract]
        [WebInvoke(UriTemplate = "Move?xfrom={xfrom}&yfrom={yfrom}&xto={xto}&yto={yto}")]
        XElement Move(int xfrom, int yfrom, int xto, int yto);
    }
}
