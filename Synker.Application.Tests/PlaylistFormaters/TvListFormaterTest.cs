using Shouldly;
using Synker.Application.PlaylistFormaters;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace Synker.Application.Tests.PlaylistFormaters
{
    public class TvListFormaterTest
    {
        [Fact]
        public void FormatPlaylist_Empty_Test()
        {
            var pl = new Playlist();
            var result = new TvListFormater().Visit(pl);

            result.ShouldBeEmpty();
        }

        [Fact]
        public void FormatPlaylist_NotEmpty_Test()
        {
            var pl = new Playlist();
            pl.AddMedia(new Media { Id = 1, DisplayName = "channel one", Url = UriAddress.For("http://pl1.synker.ovh") });
            var result = new TvListFormater().Visit(pl);

            result.ShouldStartWith(pl.Medias.FirstOrDefault()?.DisplayName);
            result.EndsWith(pl.Medias.FirstOrDefault()?.Url.Url);
            result.ShouldNotBeNullOrEmpty();
        }
    }
}
