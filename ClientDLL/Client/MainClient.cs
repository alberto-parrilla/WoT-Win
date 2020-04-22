using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public class MainClient : IMainClient
    {
        private IClient _client;

        public MainClient()
        {
            _client = new NetworkClient();
        }

        public void Init()
        {
            _client.StartClient(ReceiveResponse);
        }

        private void ReceiveResponse(IResponse response)
        {
            if (response is PingResponse)
            {
                
            }
        }

        public IResponse Send(IRequest request)
        {
            var response = _client.SendAsync(new PingRequest());
            return response;
        }
    }
}
