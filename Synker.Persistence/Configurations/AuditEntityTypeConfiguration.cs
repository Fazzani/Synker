namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities.Core;
    public class AuditEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityAudit
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("DateTime(Kind=UTC,Unspecified)");

            builder.Property(x => x.UpdatedDate)
                .HasDefaultValueSql("DateTime(Kind=UTC,Unspecified)");
        }
    }
}
