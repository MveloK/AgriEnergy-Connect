using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAPD7111_Agri_Energy_Connect.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ProductType { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [ForeignKey("Farmer")]
        public int FarmerId { get; set; }

        public FarmerModel Farmer { get; set; }
    }
}
