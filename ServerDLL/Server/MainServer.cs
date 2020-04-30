using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ServerDLL.Server
{
    public class MainServer : IMainServer
    {
        private IServer _server;
        public bool IsRunning { get; private set; }

        public MainServer()
        {
            _server = new NetworkServer(this);
        }

        public void Start()
        {
            IsRunning = true;
            _server.Start();
        }

        public void Stop()
        {
            IsRunning = false;
            _server.Stop();
        }

        public async Task ProcessRequestAsync(RequestMessageBase request, Receiver clientrReceiver)
        {
            var response = await request.ProcessRequestAsync();
            clientrReceiver.SendMessage(response);
        }
    }
}