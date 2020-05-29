using System.Data.Entity;
using CoreDatabase.Context.Data;
using CoreDatabase.Context.Data.Configuration;

namespace CoreDatabase.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext()
        {
            Database.SetInitializer(new DataDbContextInitializer());
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Transition> Transitions { get; set; }

        public DbSet<Race> Races { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Feat> Feats { get; set; }
        public DbSet<Weave> Weaves { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Area
            modelBuilder.Configurations.Add(new AreaConfiguration());

            // Scene
            modelBuilder.Configurations.Add(new SceneConfiguration());

            // Transition
            modelBuilder.Configurations.Add(new TransitionConfiguration());

            // Race
            modelBuilder.Configurations.Add(new RaceConfiguration());

            // Location
            modelBuilder.Configurations.Add(new LocationConfiguration());

            // Skill

            // Feat

            // Weave
        }
    }
}
