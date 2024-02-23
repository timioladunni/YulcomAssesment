using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YulcomAssesment.API.Models.Application
{
    public class GenerateTokenRequestDto
    {
        [Required]
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        [Required]
        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
