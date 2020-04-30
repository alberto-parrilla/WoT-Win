using System;
using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class PingRequest : RequestMessageBase
    {
        public override async Task<IResponse> ProcessRequestAsync()
        {
            return new PingResponse();
        }
    }
}
