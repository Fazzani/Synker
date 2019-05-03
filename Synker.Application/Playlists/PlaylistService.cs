namespace Synker.Application.Playlists
{
    using Synker.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public class PlaylistService : IServiceBase<long, Playlist>
    {
        public Task<Playlist> CreateAsync(Playlist entity)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> UpdateAsync(long id, Playlist entity)
        {
            throw new NotImplementedException();
        }
    }
}
