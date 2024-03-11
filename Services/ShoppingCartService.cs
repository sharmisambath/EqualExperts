using ShoppingCart.Constants;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interface;

namespace ShoppingCart.Service
{
    public class ShoppingCartService
    {
        private Dictionary<Product, int> cartProducts = new Dictionary<Product, int>();
        private readonly IProductRepository _productRepository;
        public ShoppingCartService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public Dictionary<Product, int> GetCartProducts () 
        { 
            return cartProducts; 
        }

        public void AddProductToCart(Product product,int qty)
        {
            if (product != null && qty > 0)
            {
                product.Price = _productRepository.GetProductByProductName(product.Title).Result?.Price;

                var dictionaryKey = cartProducts.Keys.Where(x => x.Title != null && x.Title.Equals(product.Title)).FirstOrDefault<Product>();
                if (dictionaryKey != null )
                {
                    if(dictionaryKey.Price != product.Price)
                    {
                        cartProducts.Remove(dictionaryKey);
                        cartProducts.Add(product, qty);
                    }
                    else
                    {
                        cartProducts[product] = qty;
                    }
                }
                else
                {
                    cartProducts.Add(product, qty);
                }
            }
        }

        public Cart GetCartDetails()
        {
            Cart cart = new Cart();
            cart.Products = cartProducts;

            foreach(var product in cartProducts)
            {
                if (product.Key.Price != null && product.Key.Price > 0)
                {
                    decimal productPrice = (decimal)(product.Key.Price * product.Value);
                    cart.SubTotal = cart.SubTotal + productPrice;
                }
            }
            cart.SubTotal = RoundUpAmount(cart.SubTotal);
            cart.Tax = CalculateTotalTaxPayable(cart.SubTotal);
            cart.Total = RoundUpAmount(cart.SubTotal + cart.Tax);

            return cart;
        }

        public Boolean DeleteProductFromCart(Product product)
        {
            if(cartProducts!= null && cartProducts.ContainsKey(product))
            {
                cartProducts[product] = cartProducts[product] - 1;
                return true;
            }
            return false;
        }

        public Boolean ClearCart()
        {
            if(cartProducts != null)
            {
                cartProducts.Clear();
                return true;
            }
            return false;
        }

        private decimal CalculateTotalTaxPayable(decimal? amount)
        {
            if(amount > 0)
            {
                return RoundUpAmount((amount * ShoppingCartConstants.TaxRate) / 100);
            }
            else
            {
                return 0;
            }
        }
        private decimal RoundUpAmount(decimal? amount)
        {
            if(amount != null && amount > 0)
                return Math.Round((decimal)amount, 2);
            else 
                return 0;
        }

    }
}
    