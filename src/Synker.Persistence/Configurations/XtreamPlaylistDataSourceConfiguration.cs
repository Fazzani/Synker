namespace Synker.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Synker.Domain.Entities;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class XtreamPlaylistDataSourcePlaylistDataSourceConfiguration : PlaylistDataSourceConfiguration<XtreamPlaylistDataSource>
    {
        public override void Configure(EntityTypeBuilder<XtreamPlaylistDataSource> builder)
        {
            builder.OwnsOne(x => x.Authentication, auth =>
            {
                auth.Property(x => x.User).IsRequired().HasMaxLength(255);
                auth.Property(x => x.Password).HasMaxLength(255);
            });

            builder.OwnsOne(x => x.Server, uri =>
            {
                uri.Property(x => x.Url).IsRequired();
                uri.Ignore(x => x.Uri);
            });
        }
    }
}
