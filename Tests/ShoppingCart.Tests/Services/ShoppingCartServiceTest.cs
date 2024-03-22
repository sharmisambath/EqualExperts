using System.Collections.Generic;
using Moq;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interface;
using ShoppingCart.Service;
using Xunit;
namespace ShoppingCart.Tests
{
    public class ShoppingCartServiceTests
    {
        Mock<IProductRepository> productRepositoryMock;
        ShoppingCartService productService;
        Product product = new Product { Title = "Cheerios", Price = (decimal?)8.43 };
        int quantity = 2;

        public ShoppingCartServiceTests()
        {
            productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductByProductName(It.IsAny<string>())).ReturnsAsync(product);
            productService = new ShoppingCartService(productRepositoryMock.Object);
        }
        [Fact]
        public void GetCartProducts()
        {
            // Act
            var cartProducts = productService.GetCartProducts();

            // Assert
            Assert.Empty(cartProducts);
        }

        [Fact]
        public void AddProductToCart()
        {
            // Act
            productService.AddProductToCart(product, quantity);

            // Assert
            var cartProducts = productService.GetCartProducts();
            Assert.True(cartProducts.ContainsKey(product));
            Assert.Equal(quantity, cartProducts[product]);
        }

        [Fact]
        public void GetCartDetails()
        {
            // Arrange
            productService.AddProductToCart(product, quantity);
            
            // Act
            var cartDetails = productService.GetCartDetails();
            
            // Assert
            Assert.Equal(16.86m, cartDetails.SubTotal);
            Assert.Equal(2.11m, cartDetails.Tax);
            Assert.Equal(18.97m, cartDetails.Total);
        }

        [Fact]
        public void DeleteProductFromCart()
        {
            // Arrange
            productService.AddProductToCart(product, quantity);
            
            // Act
            var result = productService.DeleteProductFromCart(product);
            
            // Assert
            Assert.True(result);
            Assert.Equal(1, productService.GetCartProducts()[product]);
        }
        [Fact]
        public void ClearCart()
        {
            // Arrange
            productService.AddProductToCart(product, quantity);
            
            // Act
            var result = productService.ClearCart();
            
            // Assert
            Assert.True(result);
            Assert.Empty(productService.GetCartProducts());
        }

    }
}
