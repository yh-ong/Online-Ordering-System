using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages
{
    public class ShopModel : PageModel
    {
        private ApplicationDbContext _db;

        public ShopModel(ApplicationDbContext db)
        {
            _db = db;                
        }

        public class InputModel
        {
            public int ProductID { get; set; }
        }

        public InputModel Input { get; set; }

        public IEnumerable<Product> Products { get; set; }
        
        public Shop Shops { get; set; }

        public async Task OnGet(int id)
        {
            Shops = await _db.Shops.FindAsync(id);

            // Get the product's ShopID
            var _ShopID = Shops.ShopID;
            Products = await _db.Products.Where(p => p.ShopID == _ShopID).ToListAsync();
        }
    }
}
