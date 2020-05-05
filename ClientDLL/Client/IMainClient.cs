using System;
using KernelDLL.Common;
using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public interface IMainClient
    {
        void Send(IRequest request);
        event EventHandler<IResponse> OnManageResponse;

       IDataManager DataManager { get; }
    }
}
