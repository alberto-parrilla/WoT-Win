using System;
using System.Threading.Tasks;
using CoreDatabase.Context.Users;
using CoreDatabase.Managers;

namespace CoreDatabase.FakeDatabase
{
    public class FakeUserDbManager : IUserDbManager
    {
        private string _validPassword = "lO4FkzXlh+UBzEv5BhPggU8Ap7CLx8ZI/YZaKvaiLMI="; // "test" encoded and hashed
        private string _validEmail = "test@test.com";
        private string _validUsername = "test";

        public Task<bool> AuthenticateAsync(int userId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CheckAuthenticationAsync(int userId, Guid authenticationCode)
        {
            return Task.FromResult(true);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (email == _validEmail) return new User();
            return null;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (username == _validUsername) return new User();
            return null; 
        }

        public async Task<bool> LoginAsync(string usernameOrEmail, string password)
        {
            return (usernameOrEmail == _validUsername || usernameOrEmail == _validEmail) && password == _validPassword;
        }

        public Task<bool> RegisterAsync(string username, string email, string password, Guid activationCode)
        {
            return Task.FromResult(true);
        }
    }
}
