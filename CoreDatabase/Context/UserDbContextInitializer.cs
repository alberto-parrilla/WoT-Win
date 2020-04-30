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
                Password = "lO4FkzXlh+UBzEv5BhPggU8Ap7CLx8ZI/YZaKvaiLMI=",
                Status = 1,
                ActivationCode = Guid.NewGuid(),
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
