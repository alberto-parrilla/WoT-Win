using System.Data.Entity;
using CoreDatabase.Context.Game;
using CoreDatabase.Context.Game.Configuration;

namespace CoreDatabase.Context
{
    public class GameDbContext : DbContext
    {
        public GameDbContext()
        {
            Database.SetInitializer(new GameDbContextInitializer());
        }

        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Configurations.Add(new GameSessionConfiguration());

            // Player
            modelBuilder.Configurations.Add(new PlayerConfiguration());
            modelBuilder.Configurations.Add(new AttributeConfiguration());
            modelBuilder.Configurations.Add(new AttributeModConfiguration());
            modelBuilder.Configurations.Add(new PlayerSkillConfiguration());
            modelBuilder.Configurations.Add(new PlayerFeatConfiguration());
            modelBuilder.Configurations.Add(new PlayerItemConfiguration());
            modelBuilder.Configurations.Add(new PlayerWeaveConfiguration());

        }
    }
}
