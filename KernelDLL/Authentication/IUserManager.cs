using System.Threading.Tasks;

namespace KernelDLL.Authentication
{
    public interface IUserManager
    {
        Task<RegisterResult> RegisterUserAsync(RegisterUser user);
        Task<LoginResult> LoginUserAsync(string usernameOrEmail, string password);
    }
}
