namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;

    public class M3uPlaylistDataSource : PlaylistDataSource
    {
        public M3uPlaylistDataSource()
        {
            PlaylistDataSourceFormat = PlaylistDataSourceFormatEnum.M3u;
        }

        public UriAddress Uri { get; set; }
    }
}
