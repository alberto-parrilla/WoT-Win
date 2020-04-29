using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public enum EnumRequestType
    {
        Ping,
        Login,
        Register,
        Authentication
    }

    public interface IRequest
    {
        //IResponse ProcessRequest();
        Task<IResponse> ProcessRequestAsync();
    }
}
