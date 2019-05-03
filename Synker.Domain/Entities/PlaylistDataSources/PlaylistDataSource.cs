namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Infrastructure;

    /// <summary>
    /// There are 2 types of datasource
    /// - XtreamCode (Server addr, login, password)
    /// - M3u
    /// </summary>
    public class PlaylistDataSource : EntityIdAudit<long>, IAggregateRoot
    {
        public string Name { get; set; }

        public OnlineState State { get; set; }

        public User User { get; set; }

        public PlaylistDataSourceFormatEnum PlaylistDataSourceFormat { get; set; }

    }

    public enum PlaylistDataSourceFormatEnum : short
    {
        M3u = 0,
        Xtream = 1,
        TvList = 2
    }
}