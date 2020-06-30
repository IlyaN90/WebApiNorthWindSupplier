using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TentaWebApiNWSupplier.ViewModels
{
    public class SupplierMapperModel
    {
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Company is null or WAY too long")]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(30, ErrorMessage = "Provide a shorter name, under 30")]
        public string ContactName { get; set; }

        [StringLength(30, ErrorMessage = "Provide a shorter Title, under 30")]
        public string ContactTitle { get; set; }
        
        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15, ErrorMessage = "Choose a shorter country")]
        public string Country { get; set; }

        [StringLength(24, ErrorMessage = "Phone numver is WAY too long")]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }
    }
}