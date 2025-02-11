namespace FashionStoreWebApi.Models.DTOs
{
    public class PagingRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool IsAscending { get; set; }
        public string? SortBy { get; set; }

    }
}
