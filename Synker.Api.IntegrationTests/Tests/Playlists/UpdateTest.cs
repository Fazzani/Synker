using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Synker.Api.IntegrationTests.Tests.Playlists
{
    [Collection("Playlist")]
    public class UpdateTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UpdateTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
    }
}
