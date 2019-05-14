using Shouldly;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection(nameof(PlaylistCollection))]
    public class GetFormatTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetFormatTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task GetFormat_Playlist_Not_Found()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/1111/file");
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact, Order(1)]
        public async Task GetFormat_Playlist_NoContent()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/1/format");
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact, Order(2)]
        public async Task GetFormat_Playlist_M3U_OK()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/3/format");
            httpResponse.EnsureSuccessStatusCode();

            //var responseContentBytes = await httpResponse.Content.ReadAsByteArrayAsync();
            //var result = System.Text.Encoding.UTF8.GetString(responseContentBytes);
            //responseContentBytes.ShouldNotBeNull();

            httpResponse.Content.Headers.ContentType.MediaType.ShouldBe("text/plain");
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact, Order(3)]
        public async Task GetFormat_Playlist_TvList_OK()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/3/format?format=tvlist");
            httpResponse.EnsureSuccessStatusCode();

            httpResponse.Content.Headers.ContentType.MediaType.ShouldBe("text/plain");
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        }
    }
}
