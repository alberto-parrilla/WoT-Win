using System;
using System.Data.Entity;
using System.Threading.Tasks;
using CoreDatabase.Context;
using CoreDatabase.Context.Users;

namespace CoreDatabase.Managers
{
    public class UserDbManager : IUserDbManager
    {
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (var userDbContext = new UserDbContext())
            {
                return await userDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var userDbContext = new UserDbContext())
            {
                return await userDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        public async Task<bool> LoginAsync(string usernameOrEmail, string password)
        {
            using (var userDbContext = new UserDbContext())
            {
                return await userDbContext.Users.AnyAsync(u =>
                    (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password && u.Status == 1);

            }
        }

        public async Task<bool> RegisterAsync(string username, string email, string password, Guid activationCode)
        {
            try
            {
                using (var dbContext = new UserDbContext())
                {
                    var user = new User()
                    {
                        Username = username,
                        Email = email,
                        Password = password,
                        ActivationCode = activationCode
                    };

                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
