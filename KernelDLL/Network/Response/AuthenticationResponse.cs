using System;

namespace KernelDLL.Network.Response
{
    public enum EnumAuthenticationResponse
    {
        Success,
        InvalidCode,
        UndefinedError
    }

    [Serializable]
    public class AuthenticationResponse : ResponseMessageBase
    {
        public AuthenticationResponse(EnumAuthenticationResponse status)
        {
            Status = status;
        }

        public EnumAuthenticationResponse Status { get; }

        public override EnumResponseType ResponseType => EnumResponseType.Authentication;
    }
}
