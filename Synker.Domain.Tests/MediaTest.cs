namespace Synker.Domain.Tests
{
    using Shouldly;
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
    using Xunit;

    public class MediaTest
    {

        [Fact]
        public void Media_with_some_url_are_equal()
        {
            var media1 = new Media { Id = 1, DisplayName = "name", Url = UriAddress.For("http://pl1.synker.ovh") };
            var media2 = new Media { Id = 2, DisplayName = "name2", Url = UriAddress.For("http://PL1.synker.ovh") };

            Assert.Equal(media1, media2);
            Assert.True(media1 == media2);
            media1.ShouldBe(media2);
        }

        [Fact]
        public void Media_with_some_url_are_not_equal()
        {
            var media1 = new Media { Id = 1, DisplayName = "name", Url = UriAddress.For("http://pl1.synker.ovh") };
            var media2 = new Media { Id = 1, DisplayName = "name", Url = UriAddress.For("http://pl.synker.ovh") };

            Assert.NotEqual(media1, media2);
            Assert.False(media1 == media2);
            media1.ShouldNotBe(media2);
        }

        [Fact]
        public void Media_Greatthan_by_position()
        {
            var media1 = new Media { Id = 1, Position = 2, DisplayName = "name", Url = UriAddress.For("http://pl1.synker.ovh") };
            var media2 = new Media { Id = 2, Position = 0, DisplayName = "name2", Url = UriAddress.For("http://PL1.synker.ovh") };

            Assert.True(media1 > media2);
            Assert.True(media1 >= media2);
        }

        [Fact]
        public void Media_Less_by_position()
        {
            var media1 = new Media { Id = 1, Position = 2, DisplayName = "name", Url = UriAddress.For("http://pl1.synker.ovh") };
            var media2 = new Media { Id = 2, Position = 13, DisplayName = "name2", Url = UriAddress.For("http://PL1.synker.ovh") };

            Assert.True(media1 < media2);
            Assert.True(media1 <= media2);
            //media2.ShouldBeGreaterThan(media1);
        }
    }
}
