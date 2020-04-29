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

        private async void ReceiveRequest(IRequest request, Socket handler)
        {
            //IResponse response = request.ProcessRequest();
            IResponse response = await request.ProcessRequestAsync();
            _server.Send(response, handler);
        }

    }
}