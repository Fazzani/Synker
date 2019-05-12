using Shouldly;
using Synker.Application.Playlists.Commands;
using Synker.Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection(nameof(PlaylistCollection))]
    public class CreateTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task BadRequest()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/playlists", new CreatePlaylistCommand());
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact, Order(1)]
        public async Task Created()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/playlists", new CreatePlaylistCommand
            {
                Name = "test",
                State = true,
                UserId = 1
            });

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

            var location = httpResponse.Headers.Location.ToString();
            var match = Regex.Match(location, @"/api/1.0/playlists/(?<id>\d+)$", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

            match.Success.ShouldBeTrue();
            match.Groups["id"].Success.ShouldBeTrue();
            Convert.ToInt32(match.Groups["id"].Value).ShouldBeGreaterThanOrEqualTo(Data.Playlists.Count);
        }
    }
}
