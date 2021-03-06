﻿namespace Synker.Application.DataSourceReader
{
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class M3UDataSourceReader : IDataSourceReader
    {
        private readonly M3uPlaylistDataSource _dataSource;
        public const string HeaderFile = "#EXTM3U";
        public const string HeaderUrl = "#EXTINF:";

        public M3UDataSourceReader(M3uPlaylistDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public List<Media> GetMedias()
        {
            return GetMediasAsync().GetAwaiter().GetResult();
        }

        public async Task<List<Media>> GetMediasAsync(CancellationToken cancellationToken = default)
        {
            using (var httpClient = new HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(_dataSource.Uri.Url);
                using (var streamReader = new StreamReader(stream, Encoding.UTF8, false, 4096, true))
                {
                    return await PullMediasFromProviderAsync(streamReader, cancellationToken);
                }
            }
        }

        protected async Task<List<Media>> PullMediasFromProviderAsync(StreamReader streamReader, CancellationToken cancellationToken)
        {
            var listChannels = new List<Media>();
            using (var stringReader = new StringReader(await streamReader.ReadToEndAsync()))
            {
                var line = stringReader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    return listChannels;
                var i = 1;
                //var isM3u = line.Equals(HeaderFile, StringComparison.OrdinalIgnoreCase);
                while ((line = await stringReader.ReadLineAsync()) != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (line != string.Empty && line.StartsWith(HeaderUrl))
                    {
                        var tab1 = line.Split(',');
                        //var tab2 = tab1[0].Split(' ');
                        //var live = tab2.FirstOrDefault().Equals(HeaderUrl + "0") || tab2.FirstOrDefault().Equals(HeaderUrl + "-1");
                        var channel = new Media
                        {
                            DisplayName = tab1.Last().Trim(),
                            Position = i++,
                            //IsLive = live,
                            //OriginalHeaderLine = line
                        };

                        do
                        {
                            channel.Url = UriAddress.For(await stringReader.ReadLineAsync());
                        } while (string.IsNullOrWhiteSpace(channel.Url.Url));
                        // channel.StreamId = channel.StreamId;
                        GetTvg(tab1, channel);
                        listChannels.Add(channel);
                    }

                }
            }
            return listChannels;
        }

        private static void GetTvg(string[] tab1, Media channel)
        {
            channel.Tvg = new Tvg();

            foreach (var item in tab1[0].Split(' '))
            {
                var tabTags = item.Split('=');
                if (tabTags.Length > 1)
                {
                    var value = tabTags[1].Replace("\"", "");

                    channel.Tvg.Id = item.Trim().StartsWith("tvg-id") ? value : channel.Tvg.Id;
                    channel.Tvg.Logo = item.Trim().StartsWith("tvg-logo") ? value : channel.Tvg.Logo;
                    channel.Tvg.Name = item.Trim().StartsWith("tvg-name") ? value : channel.Tvg.Name;

                    if (item.Trim().StartsWith("group-title"))
                    {
                        channel.Labels.Add(new Label { Key = Media.KnowedLabelKeys.GroupKey, Value = value });
                    }
                }
            }
        }
    }
}
