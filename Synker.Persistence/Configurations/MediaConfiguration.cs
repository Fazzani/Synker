namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;
    using Synker.Domain.Entities;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class MediaConfiguration : AuditEntityTypeConfiguration<Media>
    {
        public override void Configure(EntityTypeBuilder<Media> builder)
        {
            base.Configure(builder);

            builder
              .HasOne(x => x.Playlist)
              .WithMany(p => p.Medias)
              .HasForeignKey("PlaylistId") //Shadow property
              .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.Url, uri => {
                uri.Property(x => x.Url).IsRequired();
                uri.Ignore(x => x.Uri);
            });

            builder.Property(x => x.Labels)
                .HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<Label>>(v));

            builder.HasIndex(x => x.DisplayName);
            builder.Ignore(x => x.Group);
        }
    }
}
