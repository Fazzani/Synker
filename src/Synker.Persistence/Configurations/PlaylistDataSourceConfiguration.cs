namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PlaylistDataSourceConfiguration<TEntity> : AuditEntityTypeConfiguration<TEntity> where TEntity : PlaylistDataSource
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder
                .HasOne(x => x.User)
                .WithMany(p => (IEnumerable<TEntity>)p.PlaylistDataSources)
                .HasForeignKey("UserId") //Shadow property
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasDiscriminator<PlaylistDataSourceFormatEnum>("PlaylistDataSourceType");
            builder.HasIndex(x => x.Name);
        }
    }
}
