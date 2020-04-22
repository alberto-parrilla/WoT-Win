using System.Net.Sockets;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ServerDLL.Server
{
    public class MainServer : IMainServer
    {
        private IServer _server;

        public MainServer()
        {
            _server = new NetworkServer();
        }

        public void Init()
        {
            _server.StartListening(ReceiveRequest);
        }

        private void ReceiveRequest(IRequest request, Socket handler)
        {
            if (request is PingRequest)
            {
                _server.Send(new PingResponse(100), handler);
            }
        }
    }
}
