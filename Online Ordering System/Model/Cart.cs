using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [Required]
        public int ProductID { get; set; }
        public string CartQty { get; set; }
        public double CartPrice { get; set; }
    }
}
