namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PlaylistConfiguration : AuditEntityTypeConfiguration<Playlist>
    {
        public override void Configure(EntityTypeBuilder<Playlist> builder)
        {
            base.Configure(builder);

            builder
               .HasOne(x => x.User)
               .WithMany(p => p.Playlists)
               .HasForeignKey("UserId") //Shadow property
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Name);
        }
    }
}
