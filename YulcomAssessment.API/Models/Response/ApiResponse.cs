using System.Text.Json.Serialization;

namespace YulcomAssesment.API.Models.Response
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Reference = string.Concat("N", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(1000, 9999).ToString());
            ApiVersion = "v1";
        }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("apiVersion")]
        public string ApiVersion { get; private set; }
        [JsonPropertyName("reference")]
        public string Reference { get; private set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }
        [JsonPropertyName("error")]
        public ApiError Error { get; set; }
    }
}
