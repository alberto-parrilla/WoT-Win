using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Users.Configuration
{
    public class UserConfiguration : BusinessEntityTypeConfiguration<User>
    {
        protected override void ConfigureDbColumnsFromProperties()
        {
            // Username
            this.Property(o => o.Username)
                .IsRequired()
                .HasMaxLength(50);

            // Email
            this.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(50);

            // Password
            this.Property(o => o.Password)
                .IsRequired()
                .HasMaxLength(100);

            // Status
            this.Property(o => o.Status)
                .IsRequired();
        }

        protected override void RelateEntity()
        {
        }
    }
}
