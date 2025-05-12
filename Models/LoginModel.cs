using System.ComponentModel.DataAnnotations;

namespace EAPD7111_Agri_Energy_Connect.Models
{
    public class LoginModel
    {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; } 

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            public string Role { get; set; }

            public DateTime DateCreated { get; set; } = DateTime.Now;



    }
}
