//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using Xunit;
//using Xunit.Extensions.Ordering;

//namespace Synker.Api.FunctionalTests.Tests.Playlists
//{
//    [Collection("Playlist"), Order(1)]
//    public class CreateTest : IClassFixture<CustomWebApplicationFactory<Startup>>
//    {
//        private readonly HttpClient _client;

//        public CreateTest(CustomWebApplicationFactory<Startup> factory)
//        {
//            _client = factory.CreateClient();
//        }
//    }
//}
