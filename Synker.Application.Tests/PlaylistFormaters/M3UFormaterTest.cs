using Shouldly;
using Synker.Application.PlaylistFormaters;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace Synker.Application.Tests.PlaylistFormaters
{
    public class M3UFormaterTest
    {
        [Fact]
        public void FormatPlaylist_Empty_Test()
        {
            var pl = new Playlist();
            var result = new M3UFormater().Visit(pl);

            result.ShouldBeEmpty();
        }

        [Fact]
        public void FormatPlaylist_NotEmpty_Test()
        {
            var pl = new Playlist();
            pl.AddMedia(new Media { Id = 1, DisplayName = "name", Url = UriAddress.For("http://pl1.synker.ovh") });
            var formater = new M3UFormater();
            var result = formater.Visit(pl);

            result.ShouldStartWith(formater.HeaderFile);
            result.EndsWith(pl.Medias.FirstOrDefault()?.Url.Url);
            result.ShouldNotBeNullOrEmpty();
        }
    }
}
