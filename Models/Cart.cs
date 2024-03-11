namespace ShoppingCart.Models
{
    public class Cart
    {
        public decimal? SubTotal { get; set; } = 0.0m;
        public decimal? Tax { get; set; } = 0.0m;
        public decimal? Total { get; set; } = 0.0m;

        public Dictionary<Product, int>? Products { get; set; }

    }
}
