namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;
    public class UserConfiguration : AuditEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Email).HasMaxLength(255).IsRequired();
            builder.HasIndex(x => x.Email);
        }
    }
}
