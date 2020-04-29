using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public abstract class BaseRequest : IRequest
    {
        //public abstract IResponse ProcessRequest();
        public abstract Task<IResponse> ProcessRequestAsync();
    }
}
