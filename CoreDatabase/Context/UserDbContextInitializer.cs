using System;
using System.Data.Entity;
using CoreDatabase.Context.Users;

namespace CoreDatabase.Context
{
    public class UserDbContextInitializer : CreateDatabaseIfNotExists<UserDbContext>
    {
        protected override void Seed(UserDbContext context)
        {
            var user = new User()
            {
                Username = "test",
                Email = "test@test.com",
                Password = DbUtil.ToBase64Encode("test"),
                IsActive = true,
                ActivationCode = Guid.NewGuid(),
                IsBlocked = false
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
