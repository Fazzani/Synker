using Shouldly;
using Synker.Application.Infrastructure.PagedResult;
using Synker.Application.Playlists.Queries;
using Synker.Persistence;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection(nameof(PlaylistCollection))]
    public class MediaListTest
    {
        private readonly HttpClient _client;

        public MediaListTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task MediaList_Playlist_Not_Found()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/playlists/1111/medias", new PlaylistFileQuery { Id = 1 });
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task MediaList_Playlist_Ok()
        {
            var id = Data.Playlists[4].Id;
            var httpResponse = await _client.PostAsJsonAsync($"/api/1.0/playlists/{id}/medias", new PlaylistFileQuery { Id = id });
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<PlaylistMediasViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(25);
            ds.Results.Count.ShouldBe(10);

            ds.Results.ShouldSatisfyAllConditions(() => ds.Results.Any(x => !string.IsNullOrEmpty(x.Picon)));
        }
    }
}
