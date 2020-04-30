using System;
using System.Threading.Tasks;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public interface IClientLegacy
    {
        void SendAsync(IRequest request, Action<IResponse> manageResponse);
        Task<IResponse> ReceiveAsync();
    }
}
