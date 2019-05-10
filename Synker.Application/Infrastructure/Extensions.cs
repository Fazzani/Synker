using Synker.Application.Exceptions;
using Synker.Domain.Entities;
using Xtream.Client;

namespace Synker.Application
{
    public static class Extensions
    {
        public static ConnectionInfo GetConnection(this XtreamPlaylistDataSource xtreamPlaylistDataSource)
        {
            if (xtreamPlaylistDataSource.Server == null || string.IsNullOrEmpty(xtreamPlaylistDataSource.Server.Url))
            {
                throw new XtreamConnectionInfoException("Xtream connection info error (Server can't be null)");
            }
            return new ConnectionInfo(xtreamPlaylistDataSource.Server.Url, xtreamPlaylistDataSource.Authentication?.User, xtreamPlaylistDataSource.Authentication?.Password);
        }
    }
}
