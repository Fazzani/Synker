using Shouldly;
using Synker.Application.DataSources.Commands.Create;
using Synker.Domain.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class CreateTest
    {
        private readonly HttpClient _client;

        public CreateTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, Order(0)]
        public async Task Create_DataSource_BadRequest()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources", new CreateDataSourceCommand());
            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact, Order(1)]
        public async Task Create_DataSource_Created()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources", new CreateDataSourceCommand
            {
                Name = "test",
                PlaylistDataSourceFormat = PlaylistDataSourceFormatEnum.M3u,
                State = true,
                UserId = 1
            });

            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

            //TODO: GET "Created" Status with dataSource id
        }

    }
}
