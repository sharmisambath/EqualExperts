using ShoppingCart.Models;

namespace ShoppingCart.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByProductName(string? productName);
    }
}
