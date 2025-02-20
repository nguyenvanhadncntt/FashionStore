namespace FashionStoreViewModel
{
    public class PagingData<T>
    {
        public PagingData(IList<T> items, int totalCount, int page, int pageSize)
        {
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
        }

        public IList<T> Items { get; set; } = new List<T>();
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;
    }
}
