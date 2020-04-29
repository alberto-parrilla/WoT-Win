using KernelDLL.Network.Response;

namespace KernelDLL.Authentication
{
    public class RegisterResult
    {
        public RegisterResult(EnumRegisterResponse status)
        {
            Status = status;
        }

        public EnumRegisterResponse Status { get; }
    }
}
