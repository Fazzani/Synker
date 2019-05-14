namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;

    public class M3UPlaylistDataSource : PlaylistDataSource
    {
        public M3UPlaylistDataSource()
        {
            PlaylistDataSourceFormat = PlaylistDataSourceFormatEnum.M3u;
        }

        public UriAddress Uri { get; set; }
    }
}
