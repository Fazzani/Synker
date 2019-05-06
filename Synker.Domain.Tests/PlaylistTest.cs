namespace Synker.Domain.Tests
{
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Exceptions;
    using System.Linq;
    using Xunit;
    using Shouldly;

    public class PlaylistTest
    {
        [Fact]
        public void Must_throw_DuplicatedPlaylistMediaException()
        {
            var pl = new Playlist();
            pl.AddMedia(new Media { Id = 1, DisplayName = "name", Url = UriAddress.For("http://pl1.synker.ovh") });

            Should.Throw<DuplicatedPlaylistMediaException>(() =>
            pl.AddMedia(new Media { Id = 2, DisplayName = "name2", Url = UriAddress.For("http://pl1.synker.ovh") }));
        }

        [Fact]
        public void Two_media_should_not_the_some_position()
        {
            var pl = new Playlist();
            pl.AddMedia(new Media { Id = 1, DisplayName = "name", Position = 1, Url = UriAddress.For("http://pl1.synker.ovh") });

            Should.Throw<MediaSomePositionException>(() =>
            pl.AddMedia(new Media { Id = 2, DisplayName = "name2", Position = 1, Url = UriAddress.For("http://pl.synker.ovh") }));
        }

        [Fact]
        public void PositionMedia_not_defined_Must_set_to_Max()
        {
            var pl = new Playlist();
            pl.AddMedia(new Media { Id = 1, DisplayName = "name", Position = -1, Url = UriAddress.For("http://pl1.synker.ovh") });
            pl.AddMedia(new Media { Id = 2, DisplayName = "name2", Position = -1, Url = UriAddress.For("http://pl.synker.ovh") });

            pl.Medias.First(x => x.Id == 2).Position.ShouldBe(1);
        }

        [Fact]
        public void Throw_MediaNotFoundException_if_not_exist()
        {
            var pl = new Playlist();
            pl.AddMedia(new Media { Id = 1, DisplayName = "name", Position = -1, Url = UriAddress.For("http://pl1.synker.ovh") });
            pl.AddMedia(new Media { Id = 2, DisplayName = "name2", Position = -1, Url = UriAddress.For("http://pl.synker.ovh") });
            pl.RemoveMedia(3);

            Should.Throw<MediaNotFoundException>(() => pl.RemoveMedia(3));
        }
    }
}
