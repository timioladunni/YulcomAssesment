using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YulcomAssesment.API.Models.Yulcom
{
    public class YulcomModel
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("type1")]
        public string Type1 { get; set; }

        [Required]
        [JsonPropertyName("type2")]
        public string Type2 { get; set; }

        [Required]
        [JsonPropertyName("total")]
        public string Total { get; set; }

        [Required]
        [JsonPropertyName("hp")]
        public string HP { get; set; }

        [Required]
        [JsonPropertyName("attack")]
        public string Attack { get; set; }

        [Required]
        [JsonPropertyName("defense")]
        public string Defense { get; set; }

        [Required]
        [JsonPropertyName("spAttack")]
        public string SpAttack { get; set; }

        [Required]
        [JsonPropertyName("spDefense")]
        public string SpDefense { get; set; }

        [Required]
        [JsonPropertyName("speed")]
        public string Speed { get; set; }

        [Required]
        [JsonPropertyName("generation")]
        public string Generation { get; set; }

        [Required]
        [JsonPropertyName("legendary")]
        public string Legendary { get; set; }

    }
}
