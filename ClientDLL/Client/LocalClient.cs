using System;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public class LocalClient : IClient
    {
        public Task<IResponse> ReceiveAsync()
        {
            throw new System.NotImplementedException();
        }

        //public void SendAsync(IRequest request)
        //{
        //    throw new System.NotImplementedException();
        //}

        public void SendAsync(IRequest request, Action<IResponse> manageResponse)
        {
            throw new NotImplementedException();
        }
    }
}
