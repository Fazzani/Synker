using Moq;
using Moq.Protected;
using Shouldly;
using Synker.Application.DataSourceReader;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xtream.Client;
using Xtream.Client.XtreamConnectionInfo;
using Xunit;

namespace Synker.Application.Tests.DataReaders
{
    public class XtreamDataReaderTest
    {
        [Theory]
        [InlineData("https://gist.githubusercontent.com/Fazzani/722f67c30ada8bac4602f62a2aaccff6/raw/2d73244bb4b657514417a178bef5d299c65998b6/testmn.json")]
        public async Task XtreamClient_OK(string url, CancellationToken cancellationToken = default)
        {
            var panelJsonData = await GetXtreamPanel(url);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            MockHttpClient(mockHttpClientFactory, panelJsonData);

            var xtreamJsonReader = new XtreamJsonReader(mockHttpClientFactory.Object);
            var connectionInfo = new XtBasicConnectionFactory("http://server.tes", "", "").Create();
            var panel = await xtreamJsonReader.GetFromApi<XtreamPanel>(connectionInfo, XtreamApiEnum.Panel_Api, cancellationToken);

            var mockXtreamClient = new Mock<IXtreamClient>();
            mockXtreamClient.Setup(x => x.GetPanelAsync(It.IsAny<ConnectionInfo>(), cancellationToken)).ReturnsAsync(panel);
            mockXtreamClient.Setup(x => x.GetLiveStreamsAsync(It.IsAny<ConnectionInfo>(), cancellationToken)).ReturnsAsync(panel.Available_Channels);
            mockXtreamClient.Setup(x => x.GetLiveCategoriesAsync(It.IsAny<ConnectionInfo>(), cancellationToken)).ReturnsAsync(panel.Categories.Live);

            var ds = new XtreamPlaylistDataSource
            {
                Authentication = new BasicAuthentication("user", "pwd"),
                Server = UriAddress.For("http://test.tv")
            };

            var medias = await new XtreamDataSourceReader(ds, mockXtreamClient.Object).GetMediasAsync();
            medias.ShouldNotBeNull();
        }

        private static void MockHttpClient(Mock<IHttpClientFactory> mockHttpClientFactory, string panelJsonData)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(panelJsonData),
               })
               .Verifiable();

            mockHttpClientFactory.Setup(x => x.Create()).Returns(new HttpClient(handlerMock.Object));
        }

        private static async Task<string> GetXtreamPanel(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
