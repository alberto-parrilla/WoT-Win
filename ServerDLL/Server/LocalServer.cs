using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ServerDLL.Server
{
    public class LocalServer : IServer
    {
        public Task<IRequest> ReceiveAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Send(IResponse response, Socket handler)
        {
            throw new System.NotImplementedException();
        }

        public void StartListening(Action<IRequest, Socket> receiveRequest)
        {
            throw new NotImplementedException();
        }
    }
}
