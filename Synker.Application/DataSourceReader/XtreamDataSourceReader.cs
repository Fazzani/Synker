using Synker.Application.Interfaces;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xtream.Client;

namespace Synker.Application.DataSourceReader
{
    public class XtreamDataSourceReader : IDataSourceReader
    {
        private readonly XtreamPlaylistDataSource _dataSource;
        private readonly IXtreamClient _xtreamClient;

        public XtreamDataSourceReader(XtreamPlaylistDataSource dataSource, IXtreamClient xtreamClient)
        {
            _dataSource = dataSource;
            _xtreamClient = xtreamClient;
        }

        public List<Media> GetMedias()
        {
            return GetMediasAsync().GetAwaiter().GetResult();
        }

        public async Task<List<Media>> GetMediasAsync(CancellationToken cancellationToken = default)
        {
            var panelInfo = await _xtreamClient.GetPanelAsync(cancellationToken);

            var medias = await _xtreamClient.GetLiveStreamsAsync(cancellationToken);

            return medias.Select(x => new
            {
                success = UriAddress.TryFor(panelInfo.GenerateUrlFrom(x), out UriAddress uriAddress),
                media = x,
                url = uriAddress
            }
            ).Where(x => x.success)
            .Select(m => new Media
            {
                Position = m.media.Num,
                DisplayName = m.media.Name,
                Url = m.url
            }).ToList();
        }
    }
}
