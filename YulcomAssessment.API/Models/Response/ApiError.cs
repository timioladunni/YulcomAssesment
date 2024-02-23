using System.Text.Json.Serialization;

namespace YulcomAssesment.API.Models.Response
{
    public class ApiError
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
