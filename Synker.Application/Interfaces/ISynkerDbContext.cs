namespace Synker.Application.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Synker.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    public interface ISynkerDbContext
    {
        DbSet<PlaylistDataSource> PlaylistDataSources { get; set; }

        DbSet<Playlist> Playlists { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Media> Medias { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
