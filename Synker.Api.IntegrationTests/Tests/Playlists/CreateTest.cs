using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Synker.Api.IntegrationTests.Tests.Playlists
{
    [Collection("Playlist")]
    public class CreateTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CreateTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
    }
}
