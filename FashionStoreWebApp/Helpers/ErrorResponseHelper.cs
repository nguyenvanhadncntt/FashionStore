using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using FashionStoreViewModel;

namespace FashionStoreWebApp.Helpers
{
    public class ErrorResponseHelper
    {
        public static IList<string> GetErrorMsgFromResponse(string json)
        {
            var errors = new List<string>();
            var problemDetails = JsonDocument.Parse(json);

            if (problemDetails.RootElement.TryGetProperty("errors", out var errorList))
            {
                foreach (var errorEntry in errorList.EnumerateObject())
                {
                    if (errorEntry.Value.ValueKind == JsonValueKind.String)
                    {
                        errors.Add(errorEntry.Value.GetString()!);
                    }
                    else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
                    {
                        errors.AddRange(
                            errorEntry.Value.EnumerateArray().Select(
                                e => e.GetString() ?? string.Empty)
                            .Where(e => !string.IsNullOrEmpty(e)));
                    }
                }
            } else
            {
                var msgError = problemDetails.RootElement.GetProperty("message");
                errors.Add(msgError.GetString()!);
            }

            return errors;
        }

        public static async Task<FormResult> ConvertToFormResultError(HttpResponseMessage response)
        {
            var errors = GetErrorMsgFromResponse(await response.Content.ReadAsStringAsync());

            return new FormResult
            {
                Succeeded = false,
                ErrorList = !errors.Any() ? ["Has some error happen, please contract admin"] : [.. errors]
            };
        }
    }
}
