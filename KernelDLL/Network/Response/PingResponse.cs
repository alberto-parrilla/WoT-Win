using System;

namespace KernelDLL.Network.Response
{
    [Serializable]
    public class PingResponse : ResponseMessageBase
    {
        public override EnumResponseType ResponseType => EnumResponseType.Ping;
    }
}
