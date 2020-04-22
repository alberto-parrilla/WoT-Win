using KernelDLL.Network.Request;
using KernelDLL.Network.Response;

namespace ClientDLL.Client
{
    public interface IMainClient
    {
        void Init();

        IResponse Send(IRequest request);
    }
}
