using System.Text.Json;

namespace FashionStoreWebApp.Constants
{
    public class ConstantValues
    {
        public static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public const int NOTIFY_TIME_OUT = 3;
        public const int FIRST_PAGE = 1;
        public const int PAGE_SIZE = 5;
        public const int ZERO = 0;
        public const int ONE = 1;
        public const int MAX_PAGE_SIZE = int.MaxValue;
        public static readonly int[] PAGING_NUMBERS_DEFAULT = { 1 };
        public static readonly string BACKEND_URL = "https://localhost:7012";
        public static readonly string FRONTEND_URL = "https://localhost:7083";
    }
}
