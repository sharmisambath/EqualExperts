namespace ShoppingCart.Models
{
    public class AppSettings
    {
        public PriceApiServiceSettings? PriceApiServiceSettings { get; set; }
    }
    public class PriceApiServiceSettings
    {
        public string? BaseUrl { get; set; }
        public string? ViewProductApi {  get; set; }
    }
}