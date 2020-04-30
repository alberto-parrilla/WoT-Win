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
            _client = new NetworkClient(ReceiveResponse);
            _client.Connect();
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
            _client.SendMessage(request);
        }
    }
}
