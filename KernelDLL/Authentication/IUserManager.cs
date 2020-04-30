﻿using System.Threading.Tasks;

namespace KernelDLL.Authentication
{
    public interface IUserManager
    {
        Task<RegisterResult> RegisterUserAsync(string username, string email, string password);
        Task<LoginResult> LoginUserAsync(string usernameOrEmail, string password);
        Task<AuthenticationResult> AuthenticateUserAsync(int userId, string authenticationCode);
    }
}
