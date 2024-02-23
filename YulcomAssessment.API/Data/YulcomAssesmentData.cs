using System.ComponentModel.DataAnnotations;

namespace YulcomAssesment.API.Data
{
    public class YulcomAssesmentData
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Total { get; set; }
        public string HP { get; set; }
        public string Attack { get; set; }
        public string Defense { get; set; }
        public string SpAttack { get; set; }
        public string SpDefense { get; set; }
        public string Speed { get; set; }
        public string Generation { get; set; }
        public string Legendary { get; set; }
    }
}
