using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public class EmptyRequest : BaseRequest
    {
        //public override IResponse ProcessRequest()
        //{
        //    return new EmptyResponse();
        //}
        public override async Task<IResponse> ProcessRequestAsync()
        {
           return new EmptyResponse();
        }
    }
}
