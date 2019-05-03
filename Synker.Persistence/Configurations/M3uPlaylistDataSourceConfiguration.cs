namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;

    public class M3uPlaylistDataSourceConfiguration : PlaylistDataSourceConfiguration<M3uPlaylistDataSource>
    {
        public override void Configure(EntityTypeBuilder<M3uPlaylistDataSource> builder)
        {
            builder.OwnsOne(x => x.Uri, uri => {
                uri.Property(x => x.Url).IsRequired();
                uri.Ignore(x => x.Uri);
            });
        }
    }
}
