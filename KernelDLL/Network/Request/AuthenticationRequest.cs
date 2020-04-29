using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public class AuthenticationRequest : BaseRequest
    {
        //public override IResponse ProcessRequest()
        //{
        //    return new AuthenticationResponse();
        //}
        public override async Task<IResponse> ProcessRequestAsync()
        {
           return new AuthenticationResponse();
        }
    }
}
