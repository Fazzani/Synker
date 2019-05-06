using Shouldly;
using Synker.Application.DataSources.Queries.GetListDatasource;
using Synker.Application.Infrastructure.PagedResult;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class GetListTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetListTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetList_DataSource_PageSize_Ok()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/list", new ListDatasourceQuery { PageSize=12 });

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<DatasourceLookupViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.Results.Count.ShouldBeLessThanOrEqualTo(12);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task GetList_DataSource_Ok(ListDatasourceQuery listDatasourceQuery, int expectedCount)
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/list", listDatasourceQuery);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<DatasourceLookupViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(expectedCount);
        }

        public static IEnumerable<object[]> GetData()
        {
            return new List<object[]>
            {
                new object[] { new ListDatasourceQuery (), 42},
                new object[] { new ListDatasourceQuery { CreatedFrom = DateTime.UtcNow.AddYears(-1) }, 3},
                new object[] { new ListDatasourceQuery { CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 1},
                new object[] { new ListDatasourceQuery { Name = "xt" }, 2},
                new object[] { new ListDatasourceQuery { Name = "xt" , CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 1 },
                new object[] { new ListDatasourceQuery { Name = "xt", CreatedFrom = DateTime.UtcNow.AddMinutes(-20), Enabled = true }, 1 },
                new object[] { new ListDatasourceQuery { Name = "yellowsd" , CreatedFrom = DateTime.UtcNow.AddMinutes(-20) }, 0 },
                new object[] { new ListDatasourceQuery { Enabled = true }, 22},
                new object[] { new ListDatasourceQuery { Enabled = false }, 20}
            };
        }
    }
}
