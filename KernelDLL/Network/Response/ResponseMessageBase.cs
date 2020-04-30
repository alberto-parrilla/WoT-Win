using System;

namespace KernelDLL.Network.Response
{
    [Serializable]
    public class ResponseMessageBase : IResponse
    {
        public virtual EnumResponseType ResponseType { get; private set; }
    }
}
