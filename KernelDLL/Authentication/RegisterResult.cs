using KernelDLL.Network.Response;

namespace KernelDLL.Authentication
{
    public class RegisterResult
    {
        internal RegisterResult(int? userId)
        {
            UserId = userId;
        }

        public RegisterResult(EnumRegisterResponse status) : this(null)
        {
            Status = status;
        }

        public RegisterResult(EnumRegisterResponse status, int? userId) : this(userId)
        {
            Status = status;
        }

        public EnumRegisterResponse Status { get; }
        public int? UserId { get; }
    }
}
