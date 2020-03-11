using System.Data.Entity.ModelConfiguration;

namespace CoreDatabase.Abstractions
{
    public abstract class BusinessEntityTypeConfiguration<T> : EntityTypeConfiguration<T>
        where T : class
    {
        protected BusinessEntityTypeConfiguration()
        {
            this.SetUpConfiguration();
        }

        protected abstract void RelateEntity();

        protected abstract void ConfigureDbColumnsFromProperties();

        private void SetUpConfiguration()
        {
            this.ConfigureDbColumnsFromProperties();
            this.RelateEntity();
        }
    }
}
