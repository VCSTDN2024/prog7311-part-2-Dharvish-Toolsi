using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE_Part_2.Models
{
    public class Credential
    {
        //Used as a generic data holder for authentication.
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
