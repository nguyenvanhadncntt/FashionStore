namespace FashionStoreViewModel
{
    public class PagingRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool IsAscending { get; set; }
        public string SortBy { get; set; } = "Id";

    }
}
