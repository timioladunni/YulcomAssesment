using System.ComponentModel.DataAnnotations;

namespace YulcomAssesment.API.Data
{
    public class AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string ApiUser { get; set; }
        public string EndpointCalled { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
