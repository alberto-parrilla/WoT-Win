using KernelDLL.Network.Response;

namespace KernelDLL.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResult(EnumAuthenticationResponse status)
        {
            Status = status;
        }

        public EnumAuthenticationResponse Status { get; }
    }
}
