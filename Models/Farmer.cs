using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE_Part_2.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Farm { get; set; }
        public Farmer() { }
        public Farmer(string password, string name, string surname, string farm)
        {
            Password = password;
            Name = name;
            Surname = surname;
            Farm = farm;
        }
        public ICollection<Product>? Products { get; set; }
    }
}
