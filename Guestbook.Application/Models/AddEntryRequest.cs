using System.Text.Json.Serialization;

namespace Guestbook.Application.Models
{
    public class AddEntryRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; init; }
    }
}
