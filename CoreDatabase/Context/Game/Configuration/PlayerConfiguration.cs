using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Game.Configuration
{
    public class PlayerConfiguration : BusinessEntityTypeConfiguration<Player>
    {
        protected override void ConfigureDbColumnsFromProperties()
        {
        }

        protected override void RelateEntity()
        {
        }
    }
}
