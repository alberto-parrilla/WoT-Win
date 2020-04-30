using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class RequestMessageBase : IRequest
    { 
        public virtual Task<IResponse> ProcessRequestAsync()
        {
            return Task.FromResult(new ResponseMessageBase() as IResponse);
        }
    }
}
