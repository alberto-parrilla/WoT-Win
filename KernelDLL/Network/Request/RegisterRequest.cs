using System.Threading.Tasks;
using KernelDLL.Authentication;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    public class RegisterRequest : BaseRequest
    {
        public RegisterRequest(string username, string email, string hashPassword)
        {
            Username = username;
            Email = email;
            HashPassword = hashPassword;
        }

        public string Username { get; }
        public string Email { get; }
        public string HashPassword { get; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            var userManager = new UserManager();
            var result = await userManager.RegisterUserAsync(Username, Email, HashPassword);
            return new RegisterResponse(result.Status);
        }
    }
}
