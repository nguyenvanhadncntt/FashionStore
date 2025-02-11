namespace FashionStoreWebApi.Models
{
    public class Brand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }

        public IList<Product> Products { get; set; }
    }
}
