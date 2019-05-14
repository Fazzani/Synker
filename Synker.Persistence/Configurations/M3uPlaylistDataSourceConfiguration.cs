namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class M3uPlaylistDataSourceConfiguration : PlaylistDataSourceConfiguration<M3UPlaylistDataSource>
    {
        public override void Configure(EntityTypeBuilder<M3UPlaylistDataSource> builder)
        {
            builder.OwnsOne(x => x.Uri, uri => {
                uri.Property(x => x.Url).IsRequired();
                uri.Ignore(x => x.Uri);
            });
        }
    }
}
