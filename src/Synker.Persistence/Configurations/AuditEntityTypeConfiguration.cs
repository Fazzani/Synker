namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities.Core;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AuditEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityAudit
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedDate);

            builder.Property(x => x.UpdatedDate);
        }
    }
}
