namespace YulcomAssesment.API.Configurations
{
    public class YulcomAssesmentConfiguration
    {
        public string Environment { get; set; }
        public string ApiHeader { get; set; }
        public string[] AllowedOrigins { get; set; }
        public string EncryptionKey { get; set; }
        public int JwtLifespan { get; set; }
        public string CsvPath { get; set;}
    }
}
