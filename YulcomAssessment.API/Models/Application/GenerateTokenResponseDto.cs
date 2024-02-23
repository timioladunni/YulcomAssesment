namespace YulcomAssesment.API.Models.Application
{
    public class GenerateTokenResponseDto
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
