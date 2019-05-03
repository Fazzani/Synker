namespace Synker.Application.DataSources
{
    using Synker.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public class DataSourceService : IServiceBase<long, PlaylistDataSource>
    {
        public Task<PlaylistDataSource> CreateAsync(PlaylistDataSource entity)
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistDataSource> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistDataSource> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistDataSource> UpdateAsync(long id, PlaylistDataSource entity)
        {
            throw new NotImplementedException();
        }
    }
}
