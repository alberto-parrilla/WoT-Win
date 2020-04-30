using System.Threading.Tasks;
using KernelDLL.Authentication;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public class LoginRequestLegacy : BaseRequest
    {
        public LoginRequestLegacy(string email, string hashPassword)
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
            return new LoginResponseLegacy(result.Status, result.UserId);
        }
    }
}
