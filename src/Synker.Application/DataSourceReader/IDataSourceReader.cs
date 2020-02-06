using Synker.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Application.Interfaces
{
    public interface IDataSourceReader
    {
        List<Media> GetMedias();
        Task<List<Media>> GetMediasAsync(CancellationToken cancellationToken = default);
    }
}