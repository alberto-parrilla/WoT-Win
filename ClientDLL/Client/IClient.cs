using System;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public interface IClient
    {
        void SendAsync(IRequest request, Action<IResponse> manageResponse);
        Task<IResponse> ReceiveAsync();
    }
}
