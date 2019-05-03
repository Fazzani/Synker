namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;

    public class M3uPlaylistDataSourceConfiguration : PlaylistDataSourceConfiguration<M3uPlaylistDataSource>
    {
        public void Configure(EntityTypeBuilder<M3uPlaylistDataSource> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(x => x.Uri);
        }
    }
}
