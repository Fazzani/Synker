using Shouldly;
using Synker.Application.DataSources.Commands.Update;
using Synker.Domain.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class UpdateTest
    {
        private readonly HttpClient _client;

        public UpdateTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(10)]
        public async Task BadRequest()
        {
            var httpResponse = await _client.PutAsJsonAsync("/api/1.0/datasources/1", new UpdateDataSourceCommand { Name = "tt" });
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact, Order(11)]
        public async Task Not_Found()
        {
            var httpResponse = await _client.PutAsJsonAsync("/api/1.0/datasources/111111", new UpdateDataSourceCommand { Name = "test" });
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Fact, Order(12)]
        public async Task Ok()
        {
            var httpResponse = await _client.PutAsJsonAsync("/api/1.0/datasources/2", new UpdateDataSourceCommand
            {
                Name = "test",
                PlaylistDataSourceFormat = PlaylistDataSourceFormatEnum.M3u,
                State = true
            });

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        }
    }
}
