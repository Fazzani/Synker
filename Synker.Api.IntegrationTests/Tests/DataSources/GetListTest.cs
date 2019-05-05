using Shouldly;
using Synker.Application.DataSources.Queries.GetListDatasource;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Synker.Api.IntegrationTests.Tests.DataSources
{
    [Collection("DataSource")]
    public class GetListTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetListTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task GetList_DataSource_Ok(ListDatasourceQuery listDatasourceQuery, int expectedCount)
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/list", listDatasourceQuery);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var ds = await Utilities.GetResponseContent<ListDatasourceViewModel>(httpResponse);

            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            ds.ShouldNotBeNull();
            ds.Items.Count.ShouldBe(expectedCount);
        }

        public static IEnumerable<object[]> GetData()
        {
            return new List<object[]>
            {
                new object[] { new ListDatasourceQuery (), 4},
                new object[] { new ListDatasourceQuery { CreatedFrom = DateTime.UtcNow.AddYears(-1) }, 3},
                new object[] { new ListDatasourceQuery { CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 1},
                new object[] { new ListDatasourceQuery { Name = "xt" }, 2},
                new object[] { new ListDatasourceQuery { Name = "xt" , CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 1 },
                new object[] { new ListDatasourceQuery { Name = "xt", CreatedFrom = DateTime.UtcNow.AddMinutes(-20), Enabled = true }, 1 },
                new object[] { new ListDatasourceQuery { Name = "yellowsd" , CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 0 },
                new object[] { new ListDatasourceQuery { Enabled = true }, 3},
                new object[] { new ListDatasourceQuery { Enabled = false }, 1}
            };
        }
    }
}
