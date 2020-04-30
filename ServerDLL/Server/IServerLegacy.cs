using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ServerDLL.Server
{
    public interface IServerLegacy
    {
        void StartListening(Action<IRequest, Socket> receiveRequest);
        void Send(IResponse response, Socket handler);
        Task<IRequest> ReceiveAsync();
    }
}
