using Shouldly;
using Synker.Application.DataSources.Queries;
using Synker.Application.Infrastructure.PagedResult;
using Synker.Persistence;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    //[Collection(nameof(DataSourceCollection))]
    public class MediaListTest : IClassFixture<XtreamFixture>
    {
        private HttpClient _client;

        public MediaListTest(XtreamFixture xtreamFixture)
        {
            _client = xtreamFixture.Client;
        }

        [Fact, Order(1)]
        public async Task MediaList_DataSource_Not_Found()
        {
            var dsId = 1111;
            var httpResponse = await _client.PostAsJsonAsync($"/api/1.0/datasources/{dsId}/medias", new DataSourceMediasQuery
            {
                DataSourceId = dsId
            });

            httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact, Order(0)]
        public async Task MediaList_XtreamDataSource_Ok()
        {
            var dsId = Data.DataSources[3].Id;
            var httpResponse = await _client.PostAsJsonAsync($"/api/1.0/datasources/{dsId}/medias", new DataSourceMediasQuery
            {
                DataSourceId = dsId
            });

            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<DataSourceMediasViewModel>>(httpResponse);

            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(15);
            ds.Results.Count.ShouldBe(10);
            ds.Results.ShouldSatisfyAllConditions(() => ds.Results.Any(x => !string.IsNullOrEmpty(x.Picon)));
        }

        [Fact, Order(2)]
        public async Task MediaList_M3uDataSource_Ok()
        {
            var dsId = Data.DataSources[1].Id;
            var httpResponse = await _client.PostAsJsonAsync($"/api/1.0/datasources/{dsId}/medias", new DataSourceMediasQuery
            {
                DataSourceId = dsId
            });

            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<DataSourceMediasViewModel>>(httpResponse);

            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(96);
            ds.Results.Count.ShouldBe(10);
            ds.Results.ShouldSatisfyAllConditions(() => ds.Results.Any(x => !string.IsNullOrEmpty(x.Picon)));
        }
    }
}
