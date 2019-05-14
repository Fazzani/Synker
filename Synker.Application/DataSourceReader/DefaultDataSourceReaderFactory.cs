namespace Synker.Application.DataSourceReader
{
    using Synker.Application.DataSourceReader;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using Xtream.Client;

    public class DefaultDataSourceReaderFactory : IDataSourceReaderFactory
    {
        private readonly IXtreamClient _xtreamClient;

        public DefaultDataSourceReaderFactory(IXtreamClient xtreamClient)
        {
            _xtreamClient = xtreamClient;
        }

        public IDataSourceReader Create(PlaylistDataSource playlistDataSource)
        {
            if (playlistDataSource is M3UPlaylistDataSource uPlaylistDataSource)
            {
                return new M3UDataSourceReader(uPlaylistDataSource);
            }
            else
            {
                if (playlistDataSource is XtreamPlaylistDataSource xtreamDataSource)
                {
                    return new XtreamDataSourceReader(xtreamDataSource, _xtreamClient);
                }
            }
            return null;
        }
    }
}
