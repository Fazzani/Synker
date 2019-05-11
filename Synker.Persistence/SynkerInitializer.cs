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
        private readonly Dictionary<int, User> Users = new Dictionary<int, User>();
        private readonly Dictionary<int, PlaylistDataSource> DataSources = new Dictionary<int, PlaylistDataSource>();
        private readonly Dictionary<int, Playlist> Playlists = new Dictionary<int, Playlist>();

        public static void Initialize(SynkerDbContext context)
        {
            var initializer = new SynkerInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(SynkerDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(context);
            SeedDataSources(context);
            SeedPlaylists(context);
        }

        private void SeedPlaylists(SynkerDbContext context)
        {
            Playlists.Add(1, new Playlist { User = Users[1] });
            Playlists.Add(2, new Playlist { User = Users[1] });
            Playlists.Add(3, new Playlist { User = Users[1] });
            Playlists.Add(4, new Playlist { User = Users[2] });
            Playlists.Add(5, new Playlist { User = Users[2] });

            foreach (var pl in Playlists.Values)
            {
                context.Playlists.Add(pl);
            }

            context.SaveChanges();
        }

        private void SeedDataSources(SynkerDbContext context)
        {

            DataSources.Add(0, new M3uPlaylistDataSource
            {
                User = Users[2],
                Name = "dsm3u_2",
                Uri = UriAddress.For("http://tests.synker.ovh/m3u1"),
                State = OnlineState.Disabled,
                CreatedDate = DateTime.UtcNow.AddMonths(-5)
            });

            DataSources.Add(1, new M3uPlaylistDataSource
            {
                User = Users[1],
                Name = "dsm3u",
                Uri = UriAddress.For("https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/032182a68311091617717168f22559c9993aa21a/playlist1.m3u"),
                CreatedDate = DateTime.UtcNow.AddDays(-2)
            });

            DataSources.Add(2, new XtreamPlaylistDataSource
            {
                User = Users[1],
                Name = "ds_xt_1",
                CreatedDate = DateTime.UtcNow.AddMinutes(-13)
            });

            DataSources.Add(3, new XtreamPlaylistDataSource
            {
                User = Users[2],
                Name = "ds_xt_2",
                CreatedDate = DateTime.UtcNow.AddYears(-2),
                Authentication = new BasicAuthentication("user", "pass"),
                Server = UriAddress.For("http://synkertest.fr")
            });

            for (int i = 4, j = 23; i < 23; i++, j++)
            {
                var id = i % Users.Keys.Count;

                DataSources.Add(i, new M3uPlaylistDataSource
                {
                    User = Users[id],
                    Name = $"ds_{DateTime.Now}_{i}",
                    CreatedDate = DateTime.UtcNow.AddYears(-i),
                    State = i % 2 == 0 ? OnlineState.Disabled : OnlineState.Enabled,
                    Uri = UriAddress.For($"http://tests.synker.ovh/m3u{i}"),
                });

                DataSources.Add(j, new XtreamPlaylistDataSource
                {
                    User = Users[id],
                    Name = $"ds_{DateTime.Now}_{j}",
                    CreatedDate = DateTime.UtcNow.AddYears(-j),
                    State = j % 2 == 0 ? OnlineState.Disabled : OnlineState.Enabled,
                    Authentication = new BasicAuthentication($"user{j}", $"pass{j}")
                });
            }

            foreach (var ds in DataSources.OrderBy(x => x.Key).Select(x => x.Value))
            {
                context.PlaylistDataSources.Add(ds);
            }

            context.SaveChanges();
        }

        private void SeedUsers(SynkerDbContext context)
        {
            Users.Add(0, new User { Email = "webmaster@synker.ovh" });
            Users.Add(1, new User { Email = "support@synker.ovh" });
            Users.Add(2, new User { Email = "tunisienheni@gmail.com" });
            Users.Add(3, new User { Email = "test@synker.ovh" });

            foreach (var user in Users.Values)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}
