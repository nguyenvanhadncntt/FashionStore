using System.Text.Json;

namespace FashionStoreWebApp.Constants
{
    public class ConstantValues
    {
        public static JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static int NOTIFY_TIME_OUT = 3000;
    }
}
