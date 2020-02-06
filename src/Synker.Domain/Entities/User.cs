namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Infrastructure;
    using System.Collections.Generic;

    public class User : EntityIdAudit<long>, IAggregateRoot
    {
        public User()
        {
            PlaylistDataSources = new HashSet<PlaylistDataSource>();
            Playlists = new HashSet<Playlist>();
        }

        public string Email { get; set; }

        public ICollection<PlaylistDataSource> PlaylistDataSources { get; }
        public ICollection<Playlist> Playlists { get; }
    }
}
