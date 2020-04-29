using KernelDLL.Network.Response;

namespace KernelDLL.Authentication
{
    public class LoginResult
    {
        public LoginResult(int userId)
        {
            UserId = userId;
            Status = EnumLoginResponse.Success;
        }

        public LoginResult(EnumLoginResponse status)
        {
            UserId = null;
            Status = status;
        }

        public EnumLoginResponse Status { get; }
        public int? UserId { get; }
    }
}
