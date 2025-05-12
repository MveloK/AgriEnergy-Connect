using System.ComponentModel.DataAnnotations.Schema;

namespace EAPD7111_Agri_Energy_Connect.Models
{
    public class ProductFilterModel
    {

        [ForeignKey("Farmer")]
            public int FarmerId { get; set; }

            public DateTime? FromDate { get; set; }

            public DateTime? ToDate { get; set; }

            public string ProductType { get; set; }

            public List<ProductModel> Products { get; set; }

            public List<string> AvailableProductTypes { get; set; }
        


    }
}
