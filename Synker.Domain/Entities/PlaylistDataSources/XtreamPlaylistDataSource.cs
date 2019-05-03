namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;

    public class XtreamPlaylistDataSource : PlaylistDataSource
    {
        public XtreamPlaylistDataSource()
        {
            PlaylistDataSourceFormat = PlaylistDataSourceFormatEnum.Xtream;
        }

        public UriAddress Server { get; set; }
        public BasicAuthentication Authentication { get; set; } //TODO: Gérer tt type d'authentification
    }
}
