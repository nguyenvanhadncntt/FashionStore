namespace FashionStoreWebApi.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Product> Products { get; set; }
    }
}
