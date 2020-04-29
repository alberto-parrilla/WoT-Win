using System;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public interface IMainClient
    {
        void Init();
        void Send(IRequest request);
        event EventHandler<IResponse> OnManageResponse;
    }
}
