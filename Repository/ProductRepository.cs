using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interface;
using System.Text.Json.Serialization;

namespace ShoppingCart.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;
        public ProductRepository(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {

            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings.Value;
        }

        public async Task<Product?> GetProductByProductName(string? productName)
        {
            if (productName!= null)
            {
                using (var client = _httpClientFactory.CreateClient("priceapiclient"))
                {
                    var response = await client.GetAsync(_appSettings?.PriceApiServiceSettings?.ViewProductApi?
                                                                .Replace("product", productName?.ToLower()));
                    if (response.IsSuccessStatusCode)
                    {
                        var product = JsonConvert.DeserializeObject<Product>(response.Content.ReadAsStringAsync().Result);
                        if (product != null)
                            return product;
                    }
                }
            }
            return null;
        }
    }
}
