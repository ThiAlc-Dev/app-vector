using AutoMapper;
using Data.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Service.Services;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test
{
    public class MockHttpApiExterna
    {
        [Fact]
        public async Task Chamada_Http_Mocado()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            IHttpClientFactory factory = httpClientFactoryMock.Object;

            var externalService = new UsersService(factory, null, null, Mock.Of<ILogger<UsersService>>());

            var response = await externalService.GetHttpAsync();

            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task Chamada_Http_Com_Falha()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            IHttpClientFactory factory = httpClientFactoryMock.Object;

            var externalService = new UsersService(factory, null, null, Mock.Of<ILogger<UsersService>>());

            var response = await externalService.GetHttpAsync().ConfigureAwait(false);

            Assert.Empty(response);
        }
    }
}
