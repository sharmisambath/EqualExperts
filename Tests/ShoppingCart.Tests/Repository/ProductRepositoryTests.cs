using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Repository;
public class ProductRepositoryTests { [Fact] 
    public async Task GetProductByProductNameTest() 
    {
        //Arrange 
        var expectedProduct = new Product
        {
            Title = "Cheerios",
            Price = (decimal?)8.43
        };
        const string productName = "Cheerios";

        var appSettings = new AppSettings
        {
            PriceApiServiceSettings = new PriceApiServiceSettings { BaseUrl = "https://equalexperts.github.io", ViewProductApi = "/backend-take-home-test-data/{product}.json" }
        };

        var mockOptions = new Mock<IOptions<AppSettings>>();
        mockOptions.Setup(x => x.Value).Returns(appSettings);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedProduct))
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);
        client.BaseAddress = new Uri(appSettings.PriceApiServiceSettings.BaseUrl);
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
        
        var productRepository = new ProductRepository(mockHttpClientFactory.Object, mockOptions.Object);
        
        //Act 
        var result = await productRepository.GetProductByProductName(productName.ToLower());

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Title, result.Title);
        Assert.Equal(expectedProduct.Price, result.Price);
    }
}