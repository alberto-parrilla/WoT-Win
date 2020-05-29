using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data.Configuration
{
    public class RaceConfiguration : BusinessEntityTypeConfiguration<Race>
    {
        protected override void ConfigureDbColumnsFromProperties()
        {
        }

        protected override void RelateEntity()
        {
            //// N -> 1 Locations
            //this.HasRequired(o => o.Locations)
            //    .WithMany()
            //    .HasForeignKey(o => o.Id)
            //    .WillCascadeOnDelete(false);
        }
    }
}
