using System.Text.Json;

namespace FashionStoreWebApp.Constants
{
    public class ConstantValues
    {
        public static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public const int NOTIFY_TIME_OUT = 5;
        public const int FIRST_PAGE = 1;
        public const int PAGE_SIZE = 5;
        public const int ZERO = 0;
        public static readonly int[] PAGING_NUMBERS_DEFAULT = { 1 };
    }
}
