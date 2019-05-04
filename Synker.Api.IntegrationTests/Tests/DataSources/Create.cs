using Shouldly;
using Synker.Application.DataSources.Commands.Create;
using Synker.Domain.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Synker.Api.IntegrationTests.Tests
{
    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_DataSource_BadRequest()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources", new CreateDataSourceCommand());
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_DataSource_Ok()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources", new CreateDataSourceCommand
            {
                Name = "test",
                PlaylistDataSourceFormat = PlaylistDataSourceFormatEnum.M3u,
                State = true,
                UserId = 1
            });

            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            //TODO: GET "Created" Status with dataSource id
        }
    }
}
