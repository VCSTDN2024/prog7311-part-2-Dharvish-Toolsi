using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311_POE_Part_2.Models
{
    public class Product
    {
        // Primary Key
        [Key]
        public int Id { get; set; }
        // Atributes
        public string Name { get; set; }
        public string Category { get; set; }
        public string Farm { get; set; }
        public DateTime ProductionDate { get; set; }
        // Foreign Key
        [ForeignKey("Farmer")]
        public int FarmerId { get; set; }
        // Constructors
        public Product() { }
        public Product(string name, string category, string farm, DateTime productionDate, int farmerId)
        {
            name = Name;
            category = Category;
            farm = Farm;
            productionDate = ProductionDate;
            farmerId = FarmerId;
        }
    }
}
