using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public class PingRequestLegacy : BaseRequest
    {
        //public override IResponse ProcessRequest()
        //{
        //    return new PingResponse(0);
        //}
        public override async Task<IResponse> ProcessRequestAsync()
        {
            return new PingResponseLegacy(0);
        }
    }
}
