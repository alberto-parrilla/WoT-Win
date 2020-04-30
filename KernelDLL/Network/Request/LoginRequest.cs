using System;
using System.Threading.Tasks;
using KernelDLL.Authentication;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class LoginRequest : RequestMessageBase
    {
        public LoginRequest(string email, string hashPassword)
        {
            Email = email;
            HashPassword = hashPassword;
        }

        public string Email { get; }
        public string HashPassword { get; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            var userManager = new UserManager();
            var result = await userManager.LoginUserAsync(Email, HashPassword);
            return new LoginResponse(result.Status, result.UserId);
        }
    }
}
