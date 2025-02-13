namespace FashionStoreWebApi.Models.DTOs
{
    public class PagingData<T>
    {
        public PagingData(IList<T> items, int totalCount, int page, int pageSize)
        {
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double) pageSize);
            Items = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        IList<T> Items { get; set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
