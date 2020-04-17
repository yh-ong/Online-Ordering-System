using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [Required]
        public int Qty { get; set; }
        public int Price { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
    }
}
