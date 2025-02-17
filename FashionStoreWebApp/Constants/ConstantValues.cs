using System.Text.Json;

namespace FashionStoreWebApp.Constants
{
    public class ConstantValues
    {
        public static JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }
}
