using System.Threading.Tasks;
using KernelDLL.Network.Request;

namespace ServerDLL.Server
{
    public interface IMainServer
    {
        void Start();
        void Stop();
        bool IsRunning { get; }
        Task ProcessRequestAsync(RequestMessageBase request, Receiver client);
    }
}
