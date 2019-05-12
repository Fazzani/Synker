using Microsoft.EntityFrameworkCore;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Synker.Persistence
{
    [ExcludeFromCodeCoverage]
    public class SynkerInitializer
    {
        public static void Initialize(SynkerDbContext context)
        {
            var initializer = new SynkerInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(SynkerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(context);
            SeedDataSources(context);
            SeedPlaylists(context);

            context.SaveChanges();

        }

        private void SeedPlaylists(SynkerDbContext context)
        {
            if(Data.Playlists.Count == 0)
            {
                Data.Playlists.Add(1, new Playlist { Name = "playlist1", User = Data.Users[1] });
                Data.Playlists.Add(2, new Playlist { Name = "test", User = Data.Users[1] });
                Data.Playlists.Add(3, new Playlist { Name = "pl", User = Data.Users[1] });
                Data.Playlists.Add(4, new Playlist { Name = "vip", User = Data.Users[2] });
                Data.Playlists.Add(5, new Playlist { Name = "max", User = Data.Users[2], State = OnlineState.Disabled });

                for (int i = 1; i < 50; i++)
                {
                    Data.Medias.Add(i, new Media { DisplayName = $"Medias {i}", Position = i, Url = UriAddress.For($"http://playlist/media{i}.ts") });
                }

                Data.Playlists[4].AddRangeMedia(Data.Medias.Skip(0).Take(25).Select(x => x.Value).ToList());
                Data.Playlists[2].AddRangeMedia(Data.Medias.Skip(25).Take(15).Select(x => x.Value).ToList());
                Data.Playlists[3].AddRangeMedia(Data.Medias.Skip(40).Take(5).Select(x => x.Value).ToList());
            }

            foreach (var pl in Data.Playlists.OrderBy(x => x.Key).Select(x => x.Value))
            {
                context.Playlists.Add(pl);
            }

        }

        private void SeedDataSources(SynkerDbContext context)
        {

            Data.DataSources.TryAdd(0, new M3uPlaylistDataSource
            {
                User = Data.Users[2],
                Name = "dsm3u_2",
                Uri = UriAddress.For("http://tests.synker.ovh/m3u1"),
                State = OnlineState.Disabled,
                CreatedDate = DateTime.UtcNow.AddMonths(-5)
            });

            Data.DataSources.TryAdd(1, new M3uPlaylistDataSource
            {
                User = Data.Users[1],
                Name = "dsm3u",
                Uri = UriAddress.For("https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/032182a68311091617717168f22559c9993aa21a/playlist1.m3u"),
                CreatedDate = DateTime.UtcNow.AddDays(-2)
            });

            Data.DataSources.TryAdd(2, new XtreamPlaylistDataSource
            {
                User = Data.Users[1],
                Name = "ds_xt_1",
                Server = UriAddress.For("https://gist.githubusercontent.com"),
                CreatedDate = DateTime.UtcNow.AddMinutes(-13),
                Authentication = new BasicAuthentication("user_test", "pass_test")
            });

            Data.DataSources.TryAdd(3, new XtreamPlaylistDataSource
            {
                User = Data.Users[2],
                Name = "ds_xt_2",
                CreatedDate = DateTime.UtcNow.AddYears(-2),
                Authentication = new BasicAuthentication("user", "pass"),
                Server = UriAddress.For("http://synkertest.fr")
            });

            for (int i = 4, j = 23; i < 23; i++, j++)
            {
                var id = i % Data.Users.Keys.Count;

                Data.DataSources.TryAdd(i, new M3uPlaylistDataSource
                {
                    User = Data.Users[id],
                    Name = $"ds_{DateTime.Now}_{i}",
                    CreatedDate = DateTime.UtcNow.AddYears(-i),
                    State = i % 2 == 0 ? OnlineState.Disabled : OnlineState.Enabled,
                    Uri = UriAddress.For($"http://tests.synker.ovh/m3u{i}"),
                });

                Data.DataSources.TryAdd(j, new XtreamPlaylistDataSource
                {
                    User = Data.Users[id],
                    Name = $"ds_{DateTime.Now}_{j}",
                    CreatedDate = DateTime.UtcNow.AddYears(-j),
                    State = j % 2 == 0 ? OnlineState.Disabled : OnlineState.Enabled,
                    Server = UriAddress.For($"http://synkertest{j}.fr"),
                    Authentication = new BasicAuthentication($"user{j}", $"pass{j}")
                });
            }

            foreach (var ds in Data.DataSources.OrderBy(x => x.Key).Select(x => x.Value))
            {
                context.PlaylistDataSources.Add(ds);
            }

        }

        private void SeedUsers(SynkerDbContext context)
        {
            Data.Users.TryAdd(0, new User { Email = "webmaster@synker.ovh" });
            Data.Users.TryAdd(1, new User { Email = "support@synker.ovh" });
            Data.Users.TryAdd(2, new User { Email = "tunisienheni@gmail.com" });
            Data.Users.TryAdd(3, new User { Email = "test@synker.ovh" });

            foreach (var user in Data.Users.OrderBy(x => x.Key).Select(x => x.Value))
            {
                context.Users.Add(user);
            }

        }
    }

    public static class Data
    {
        public static Dictionary<int, User> Users = new Dictionary<int, User>();
        public static Dictionary<int, PlaylistDataSource> DataSources = new Dictionary<int, PlaylistDataSource>();
        public static Dictionary<int, Playlist> Playlists = new Dictionary<int, Playlist>();
        public static Dictionary<int, Media> Medias = new Dictionary<int, Media>();
    }
}
