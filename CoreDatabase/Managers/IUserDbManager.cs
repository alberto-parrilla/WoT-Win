using System;
using System.Threading.Tasks;
using CoreDatabase.Context.Users;

namespace CoreDatabase.Managers
{
    public interface IUserDbManager
    {
        Task<bool> LoginAsync(string usernameOrEmail, string password);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> RegisterAsync(string username, string email, string password, Guid activationCode);
    }
}
