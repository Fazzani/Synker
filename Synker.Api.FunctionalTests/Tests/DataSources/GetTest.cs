using Shouldly;
using Synker.Application.DataSources.Queries.GetDatasource;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class GetTest
    {
        private readonly HttpClient _client;

        public GetTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task Get_DataSource_Ok()
        {
            long id = 3;
            var httpResponse = await _client.GetAsync($"/api/1.0/datasources/{3}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var ds = await Utilities.GetResponseContent<DataSourceViewModel>(httpResponse);
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            ds.ShouldNotBeNull();
            ds.Id.ShouldBe(id);
        }

        [Fact, Order(1)]
        public async Task Get_Not_FoundException()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/datasources/1000");
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }
    }
}
