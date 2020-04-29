using System;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public class MainClient : IMainClient
    {
        private IClient _client;
        public event EventHandler<IResponse> OnManageResponse;

        public MainClient()
        {
            _client = new NetworkClient();
        }

        public void Init()
        {
            //_client.StartClient(ReceiveResponse);
        }

        private void ReceiveResponse(IResponse response)
        {
            if (OnManageResponse != null)
            {
                OnManageResponse(this, response);
            }
        }

        public void Send(IRequest request)
        {
            //_client.StartClient();
            _client.SendAsync(request, ReceiveResponse);
            //_client.StopClient();
        }
    }
}
