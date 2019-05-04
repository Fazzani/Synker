using Shouldly;
using Synker.Application.DataSources.Queries.GetDatasource;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Synker.Api.IntegrationTests.Tests
{
    public class DataSourceControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public DataSourceControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_DataSource_Ok()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/datasources/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var ds = await Utilities.GetResponseContent<DataSourceViewModel>(httpResponse);
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            ds.ShouldNotBeNull();
            ds.Id.ShouldBe(1);
        }

        [Fact]
        public async Task Get_Not_FoundException()
        {
            var httpResponse = await _client.GetAsync("/api/1.0/datasources/1000");
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }
    }
}
