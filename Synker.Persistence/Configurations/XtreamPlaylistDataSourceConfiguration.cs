namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;
    public class XtreamPlaylistDataSourcePlaylistDataSourceConfiguration : PlaylistDataSourceConfiguration<XtreamPlaylistDataSource>
    {
        public void Configure(EntityTypeBuilder<XtreamPlaylistDataSource> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(x => x.Server);
            builder.OwnsOne(x => x.Authentication);
        }
    }
}
