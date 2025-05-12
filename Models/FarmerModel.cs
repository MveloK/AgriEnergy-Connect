using System.ComponentModel.DataAnnotations;

namespace EAPD7111_Agri_Energy_Connect.Models
{
    public class FarmerModel
    {
            [Key]
            public int FarmerId { get; set; }

            [Required]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }

            public string Address { get; set; }

        public string UserId { get; set; }

        public List<ProductModel> Products { get; set; } = new List<ProductModel>();



    }
}
