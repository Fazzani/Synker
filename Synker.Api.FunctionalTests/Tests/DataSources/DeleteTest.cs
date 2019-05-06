using Shouldly;
using Synker.Application.DataSources.Commands.Update;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class DeleteTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public DeleteTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task Delete_DataSource_Not_Found()
        {
            var httpResponse = await _client.PutAsJsonAsync("/api/1.0/datasources/111111", new UpdateDataSourceCommand { Name = "test" });
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Fact, Order(1)]
        public async Task Delete_DataSource_Ok()
        {
            var httpResponse = await _client.DeleteAsync("/api/1.0/datasources/1");
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        }
    }
}
