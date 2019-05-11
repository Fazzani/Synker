using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;
using Synker.Application.DataSources.Queries;
using Synker.Application.Infrastructure.PagedResult;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xtream.Client;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests.Tests.DataSources
{
    [Collection(nameof(DataSourceCollection))]
    public class MediaListTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public MediaListTest(CustomWebApplicationFactory<Startup> factory)
        {
            var serviceDescriptorXtreamClient = new ServiceDescriptor(typeof(IXtreamClient), typeof(XtreamClientMoq), ServiceLifetime.Transient);
            _client = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices((IServiceCollection services) =>
                    {
                        services.Replace(serviceDescriptorXtreamClient);
                    }))
                .CreateClient();
        }

        [Fact, Order(0)]
        public async Task MediaList_DataSource_Not_Found()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/1111/medias", new DataSourceMediasQuery { DataSourceId = 1 });
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task MediaList_XtreamDataSource_Ok()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/4/medias", new DataSourceMediasQuery { DataSourceId = 1 });
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<DataSourceMediasViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(15);
            ds.Results.Count.ShouldBe(10);

            ds.Results.ShouldSatisfyAllConditions(() => ds.Results.Any(x => !string.IsNullOrEmpty(x.Picon)));
        }

        [Fact]
        public async Task MediaList_M3uDataSource_Ok()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/2/medias", new DataSourceMediasQuery { DataSourceId = 1 });
            httpResponse.EnsureSuccessStatusCode();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var ds = await Utilities.GetResponseContent<PagedResult<DataSourceMediasViewModel>>(httpResponse);
            ds.ShouldNotBeNull();
            ds.RowCount.ShouldBe(96);
            ds.Results.Count.ShouldBe(10);

            ds.Results.ShouldSatisfyAllConditions(() => ds.Results.Any(x => !string.IsNullOrEmpty(x.Picon)));
        }

        [Fact]
        public async Task MediaList_DataSource_ConnectionInfo_Server_null_500()
        {
            var httpResponse = await _client.PostAsJsonAsync("/api/1.0/datasources/3/medias", new DataSourceMediasQuery { DataSourceId = 1 });
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            httpResponse.ReasonPhrase.Equals("Xtream connection info error (Server can't be null)", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
