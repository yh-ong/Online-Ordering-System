using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class Shop
    {
        [Key]
        public int ShopID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime DateAdded { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User Users { get; set; }
    }
}
