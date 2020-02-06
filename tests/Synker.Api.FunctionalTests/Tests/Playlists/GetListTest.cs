using Shouldly;
using Synker.Application.Infrastructure.PagedResult;
using Synker.Application.Playlists.Queries;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.Playlists
{
    [Collection(nameof(PlaylistCollection))]
    public class GetListTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetListTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetList_Playlists_PageSize_Ok()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/playlists/list", new ListPlaylistQuery { PageSize=12 });

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<PlaylistLookupViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.Results.Count.ShouldBeLessThanOrEqualTo(12);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task GetList_Playlists_Ok(ListPlaylistQuery listDatasourceQuery, int expectedCount)
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/playlists/list", listDatasourceQuery);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<PlaylistLookupViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(expectedCount);
        }

        public static IEnumerable<object[]> GetData()
        {
            return new List<object[]>
            {
                new object[] { new ListPlaylistQuery (), 5},
                //new object[] { new ListPlaylistQuery { CreatedFrom = DateTime.UtcNow.AddYears(-1) }, 5},
                //new object[] { new ListPlaylistQuery { CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 5},
                new object[] { new ListPlaylistQuery { Name = "pl" }, 2},
                //new object[] { new ListPlaylistQuery { Name = "xt" , CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 1 },
                //new object[] { new ListPlaylistQuery { Name = "xt", CreatedFrom = DateTime.UtcNow.AddMinutes(-20), Enabled = true }, 1 },
                //new object[] { new ListPlaylistQuery { Name = "yellowsd" , CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 0 },
                new object[] { new ListPlaylistQuery { Enabled = true }, 4},
                new object[] { new ListPlaylistQuery { Enabled = false }, 1}
            };
        }
    }
}
