using Synker.Application.DataSourceReader;
using Synker.Domain.Entities;
using Xtream.Client;

namespace Synker.Application.Interfaces
{
    public class DefaultDataSourceReaderFactory : IDataSourceReaderFactory
    {
        private IXtreamClient _xtreamClient;

        public DefaultDataSourceReaderFactory(IXtreamClient xtreamClient)
        {
            _xtreamClient = xtreamClient;
        }

        public IDataSourceReader Create(PlaylistDataSource playlistDataSource)
        {
            if (playlistDataSource is M3uPlaylistDataSource uPlaylistDataSource)
            {
                return new M3uDataSourceReader(uPlaylistDataSource);
            }
            else
             if (playlistDataSource is XtreamPlaylistDataSource xtreamDataSource)
            {
                return new XtreamDataSourceReader(xtreamDataSource, _xtreamClient);
            }
            return null;
        }
    }
}
