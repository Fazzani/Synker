using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xtream.Client;

namespace Synker.Api.FunctionalTests.Tests
{
    public class XtreamClientMoq : IXtreamClient
    {
        private XtreamPanel _xtreamPanel;

        public XtreamClientMoq()
        {
            _xtreamPanel = Newtonsoft.Json.JsonConvert.DeserializeObject<XtreamPanel>(TrimJsonAndConvertChannelsToArray(GetXtreamPanel()));
        }

        private static string TrimJsonAndConvertChannelsToArray(string jsonContent)
        {
            jsonContent = Regex.Replace(jsonContent, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");
            jsonContent = Regex.Replace(jsonContent, "\"\\d+\":{", "{", RegexOptions.Multiline);
            jsonContent = jsonContent.Replace("\"available_channels\":{", "\"available_channels\":[");
            jsonContent = jsonContent.Replace("}}}", "}]}");
            return jsonContent;
        }

        private static string GetXtreamPanel(string url = "https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/2d73244bb4b657514417a178bef5d299c65998b6/testmn.json")
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }

        public Task<List<Epg_Listings>> GetAllEpgAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<List<Live>> GetLiveCategoriesAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => _xtreamPanel.Categories.Live);
        }

        public Task<List<Channels>> GetLiveStreamsAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => _xtreamPanel.Available_Channels);
        }

        public Task<List<Channels>> GetLiveStreamsByCategoriesAsync(ConnectionInfo connectionInfo, string categoryId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => _xtreamPanel.Available_Channels.Where(c => c.Category_id == categoryId).ToList());
        }

        public Task<XtreamPanel> GetPanelAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => _xtreamPanel);
        }

        public Task<List<Epg_Listings>> GetShortEpgForStreamAsync(ConnectionInfo connectionInfo, string streamId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => new List<Epg_Listings> { new Epg_Listings { Channel_id = streamId, Description = "epg 1", Title = "title1" } });
        }

        public Task<PlayerApi> GetUserAndServerInfoAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => new PlayerApi { Server_info = _xtreamPanel.Server_info, User_info = _xtreamPanel.User_info });
        }

        public Task<List<Channels>> GetVodStreamsAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() => _xtreamPanel.Available_Channels.Where(x => x.MediaType != MediaType.LiveTv).ToList());
        }

        public Task<PlayerApi> GetXmltvAsync(ConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            //TODO: bizarre
            return Task.Run(() => new PlayerApi { Server_info = _xtreamPanel.Server_info, User_info = _xtreamPanel.User_info });
        }
    }
}
