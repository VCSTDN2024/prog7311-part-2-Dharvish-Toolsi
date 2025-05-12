using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE_Part_2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Farm { get; set; }
        public DateTime ProductionDate { get; set; }
    }
}
