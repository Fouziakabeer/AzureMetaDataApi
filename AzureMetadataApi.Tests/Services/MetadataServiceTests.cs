using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AzureMetadataApi.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace AzureMetadataApi.Tests.Services
{
    public class MetadataServiceTests
    {
        [Fact]
        public async Task GetMetadataByPathAsync_ReturnsExpectedValue()
        {
            var expectedJson = "{\"compute\":{\"name\":\"test-vm\"}}";
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedJson),
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://169.254.169.254")
            };

            var cache = new MemoryCache(new MemoryCacheOptions());
            var logger = new Mock<ILogger<MetadataService>>();

            var service = new MetadataService(httpClient, cache, logger.Object);
            var result = await service.GetMetadataByPathAsync("compute.name");

            Assert.Equal("test-vm", result);
        }
    }
}
