using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class Seller
    {
        [Key]
        public int SellerID { get; set; }

        [Required]
        public string SellerName { get; set; }
        
        [Required]
        [EmailAddress]
        public string SellerEmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string SellerPassword { get; set; }

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCity { get; set; }

        [DataType(DataType.PostalCode)]
        public string CompanyPostalCode { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
