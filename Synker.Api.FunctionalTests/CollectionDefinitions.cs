using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Synker.Api.FunctionalTests
{
    [CollectionDefinition(nameof(PlaylistCollection)), Order(2)]
    public class PlaylistCollection { }

    [CollectionDefinition(nameof(PlaylistCollection)), Order(1)]
    public class DataSourceCollection { }
}
