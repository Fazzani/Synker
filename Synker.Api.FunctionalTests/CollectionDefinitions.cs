using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests
{
    [CollectionDefinition(nameof(PlaylistCollection)), Order(2)]
    public class PlaylistCollection: ICollectionFixture<CustomWebApplicationFactory<Startup>> { }

    [CollectionDefinition(nameof(DataSourceCollection)), Order(1)]
    public class DataSourceCollection : ICollectionFixture<CustomWebApplicationFactory<Startup>> { }

}
