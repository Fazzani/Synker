using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Synker.Api.FunctionalTests.Tests;
using Xtream.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using Synker.Persistence;

namespace Synker.Api.FunctionalTests
{
    public class XtreamFixture : CustomWebApplicationFactory<Startup>
    {
       public HttpClient Client;

        public XtreamFixture()
        {
            var serviceDescriptorXtreamClient = new ServiceDescriptor(typeof(IXtreamClient), typeof(XtreamClientMoq), ServiceLifetime.Transient);
            Client = this
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices((IServiceCollection services) =>
                    {
                        services.Replace(serviceDescriptorXtreamClient);
                       
                    }))
                .CreateClient();
        }
    }
}
