using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Synker.Persistence;
using Xtream.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Synker.Api.FunctionalTests.Tests;
using System;

namespace Synker.Api.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        private bool _inMemory = false;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceCollection = new ServiceCollection();

                if (_inMemory)
                    serviceCollection.AddEntityFrameworkInMemoryDatabase();
                else
                    serviceCollection.AddEntityFrameworkSqlite();

                var serviceProvider = serviceCollection
                    .BuildServiceProvider();

                services.AddTransient<IXtreamClient, XtreamClientMoq>();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                if(_inMemory)
                {
                    services.AddDbContext<SynkerDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("playlist_ddd_dev")
                               .UseInternalServiceProvider(serviceProvider);
                    });
                }
                else
                {
                    services.AddDbContext<SynkerDbContext>(options =>
                    {
                        options.UseSqlite($"DataSource=testdb")//:memory: not working because we have to open/close connection manually
                               .EnableSensitiveDataLogging(true)
                               .UseInternalServiceProvider(serviceProvider);
                    });
                }

                // Build the service provider.
                var sp = services.BuildServiceProvider();
                
                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<SynkerDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    try
                    {
                        SynkerInitializer.Initialize(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                    //finally
                    //{
                    //    appDb.Database.CloseConnection();
                    //}
                }
            });
        }
    }
}
