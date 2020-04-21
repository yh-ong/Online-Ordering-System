using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Ordering_System.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        public int QtyInStock { get; set; }
        
        [Required]
        public double Price { get; set; }

        [Required]
        public int ShopID { get; set; } // Foreign Key

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("ShopID")] // Navigate ForeginKey Parameter
        public Shop Shops { get; set; } // Point the PrimaryKey's Table

        [ForeignKey("CategoryID")]
        public Category Categories { get; set; }
    }
}
