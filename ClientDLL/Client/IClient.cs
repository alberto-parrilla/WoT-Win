using System;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public interface IClient
    {
        void StartClient(Action<IResponse> manageResponse);
        IResponse SendAsync(IRequest request);
        Task<IResponse> ReceiveAsync();
    }
}
