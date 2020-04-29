using System.Data.Entity;

namespace CoreDatabase.Context
{
    public class UserDbContextInitializer : CreateDatabaseIfNotExists<UserDbContext>
    {
        protected override void Seed(UserDbContext context)
        {
            //var user = new User()
            //{
            //    Username = "test",
            //    Email = "test@test.com",
            //    Password = DbUtil.ToBase64Encode("test"),
            //    ActivationCode = Guid.NewGuid(),
            //};

            //context.Users.Add(user);
            //context.SaveChanges();
        }
    }
}
