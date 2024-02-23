using System.ComponentModel.DataAnnotations;

namespace YulcomAssesment.API.Data
{
    public class ApiKey
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string PublicApiKey { get; set; }

        [Required]
        public string SecretApiKey { get; set; }
    }
}
