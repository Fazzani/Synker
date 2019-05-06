using Shouldly;
using Synker.Application.Playlists.Queries;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection(nameof(PlaylistCollection))]
    public class GetTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task Get_Playlist_Ok()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var ds = await Utilities.GetResponseContent<PlaylistViewModel>(httpResponse);
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            ds.ShouldNotBeNull();
            ds.Id.ShouldBe(1);
        }

        [Fact, Order(1)]
        public async Task Get_Not_FoundException()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/1000");
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }
    }
}
