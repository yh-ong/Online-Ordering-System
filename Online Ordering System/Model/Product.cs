using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int ProductName { get; set; }
        public string Description { get; set; }
        public int QtyInStock { get; set; }
        public double Price { get; set; }
    }
}
