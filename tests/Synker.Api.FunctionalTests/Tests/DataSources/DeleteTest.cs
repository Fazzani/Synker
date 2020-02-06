using Shouldly;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class DeleteTest
    {
        private readonly HttpClient _client;

        public DeleteTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task Not_Found()
        {
            var httpResponse = await _client.DeleteAsync("/api/1.0/datasources/111111");
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Fact, Order(1)]
        public async Task Delete_Ok()
        {
            var httpResponse = await _client.DeleteAsync("/api/1.0/datasources/1");
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        }
    }
}
