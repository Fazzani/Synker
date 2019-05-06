using Shouldly;
using Synker.Application.Playlists.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection("Playlist")]
    public class GetTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
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

        [Fact]
        public async Task Get_Not_FoundException()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/playlists/1000");
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }
    }
}
