using System;
using System.Threading.Tasks;
using CoreDatabase.Context.Users;
using CoreDatabase.Managers;

namespace CoreDatabase.FakeDatabase
{
    public class FakeUserDbManager : IUserDbManager
    {
        private string _validPassword = "lO4FkzXlh+UBzEv5BhPggU8Ap7CLx8ZI/YZaKvaiLMI="; // "test" encoded and hashed
        private string _validEmail = "test";

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (email == _validEmail) return new User();
            return null;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (username == _validEmail) return new User();
            return null; 
        }

        public async Task<bool> LoginAsync(string usernameOrEmail, string password)
        {
            return usernameOrEmail == _validEmail && password == _validPassword;
        }

        public Task<bool> RegisterAsync(string username, string email, string password, Guid activationCode)
        {
            throw new NotImplementedException();
        }
    }
}
