using KernelDLL.Network.Request;

namespace ClientDLL.Client
{
    public interface IClient
    {
        void Connect();
        void Disconnect();
        void SendMessage(IRequest request);
    }
}
