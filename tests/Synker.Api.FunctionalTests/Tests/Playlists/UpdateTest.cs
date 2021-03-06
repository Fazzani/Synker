﻿using Shouldly;
using Synker.Application.Playlists.Commands;
using Synker.Persistence;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection(nameof(PlaylistCollection))]
    public class UpdateTest
    {
        private readonly HttpClient _client;

        public UpdateTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task BadRequest()
        {
            var httpResponse = await _client.PutAsJsonAsync("/api/1.0/Playlists/1", new UpdatePlaylistCommand { Name = "tt" });
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact, Order(1)]
        public async Task Not_Found()
        {
            var httpResponse = await _client.PutAsJsonAsync("/api/1.0/Playlists/111111", new UpdatePlaylistCommand { Name = "test" });
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Fact, Order(2)]
        public async Task Ok()
        {
            var id = Data.Playlists[2].Id;
            var httpResponse = await _client.PutAsJsonAsync($"/api/1.0/Playlists/{id}", new UpdatePlaylistCommand
            {
                Name = "test",
                State = true
            });

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        }
    }
}
