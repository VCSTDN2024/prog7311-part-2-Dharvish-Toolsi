using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE_Part_2.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }
        private string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Farm { get; set; }

    }
}
