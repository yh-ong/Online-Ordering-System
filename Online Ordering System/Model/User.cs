using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName{ get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }
        public string Country { get; set; }
    }
}
