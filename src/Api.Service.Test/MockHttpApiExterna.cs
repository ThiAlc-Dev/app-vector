using AutoMapper;
using Data.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Service.Services;
using System;
using System.Collections.Generic;
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
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
                            [
                                {
                                    ""createdAt"": ""2021-07-26T00:52:35.262Z"",
                                    ""name"": ""Sam Kutch"",
                                    ""avatar"": ""https://cdn.fakercloud.com/avatars/plasticine_128.jpg"",
                                    ""mail"": ""Orpha.Howe@hotmail.com"",
                                    ""id"": ""1""
                                },
                                {
                                    ""createdAt"": ""2021-07-26T09:32:02.483Z"",
                                    ""name"": ""Dr. Marta McCullough"",
                                    ""avatar"": ""https://cdn.fakercloud.com/avatars/overcloacked_128.jpg"",
                                    ""mail"": ""Adella_Koch61@hotmail.com"",
                                    ""id"": ""2""
                                },
                                {
                                    ""createdAt"": ""2021-07-26T20:52:18.340Z"",
                                    ""name"": ""Jean Koch"",
                                    ""avatar"": ""https://cdn.fakercloud.com/avatars/yigitpinarbasi_128.jpg"",
                                    ""mail"": ""Morton18@gmail.com"",
                                    ""id"": ""3""
                                }
                            ]")
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
