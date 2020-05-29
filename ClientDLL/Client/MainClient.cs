using System;
using KernelDLL.Common;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public class MainClient : IMainClient
    {
        private IClient _client;
        public event EventHandler<IResponse> OnManageResponse;

        public MainClient(IDataManager dataManager)
        {
            DataManager = dataManager;
            DataContainer = new DataContainer();
            _client = new NetworkClient(ReceiveResponse);
            _client.Connect();
           
        }

        public IDataManager DataManager { get;  }
        public IDataContainer DataContainer { get;  }

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
