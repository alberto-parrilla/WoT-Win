using System.Data.Entity;
using CoreDatabase.Context.Users;
using CoreDatabase.Context.Users.Configuration;

namespace CoreDatabase.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
        {
            Database.SetInitializer(new UserDbContextInitializer());
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
