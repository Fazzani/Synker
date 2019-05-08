using Shouldly;
using Synker.Application.DataSourceReader;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Threading.Tasks;
using Xunit;

namespace Synker.Application.Tests.DataReaders
{
    public class M3uDataReaderTest
    {
        const string pl1 = "https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/032182a68311091617717168f22559c9993aa21a/playlist1.m3u";
        const string pl2 = "https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/e6c5037f38441606e749c8a0244f33181b04d346/playlist2.m3u";
        const string pl3 = "https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/cf3a2c90613f127274965589d250f5152970f4e3/playlist3.m3u";

        [Theory]
        [InlineData(pl1, 96)]
        [InlineData(pl2, 96)]
        [InlineData(pl3, 24)]
        public async Task Medias_Must_Have_DisplayNames(string url, int count)
        {
            var ds = new M3uPlaylistDataSource
            {
                Uri =
                UriAddress.For(url)
            };

            var medias = await new M3uDataSourceReader(ds).GetMediasAsync();
            medias.ShouldNotBeNull();
            medias.Count.ShouldBe(count);
            medias.ShouldAllBe(x => x.DisplayName.Length > 0);
        }
    }
}
