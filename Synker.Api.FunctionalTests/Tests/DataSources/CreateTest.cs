using Shouldly;
using Synker.Application.DataSources.Commands.Create;
using Synker.Domain.Entities;
using Synker.Persistence;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
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

            var location = httpResponse.Headers.Location.ToString();
            var match = Regex.Match(location, @"/api/1.0/datasources/(?<id>\d+)$", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

            match.Success.ShouldBeTrue();
            match.Groups["id"].Success.ShouldBeTrue();
            Convert.ToInt32(match.Groups["id"].Value).ShouldBeGreaterThanOrEqualTo(Data.DataSources.Count);
        }
    }
}
