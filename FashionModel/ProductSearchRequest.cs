namespace FashionStoreViewModel
{
    public class ProductSearchRequest
    {
        public string? name { get; set; }
        public long categoryId { get; set; }
        public long brandId { get; set; }
    }
}
