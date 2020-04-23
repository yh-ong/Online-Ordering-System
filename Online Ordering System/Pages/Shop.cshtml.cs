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

        [BindProperty]
        public Product Products { get; set; }
        
        [BindProperty]
        public Shop Shops { get; set; }

        public async Task OnGet(int id)
        {
            Products = await _db.Products.FindAsync(id);

            // Get the product's ShopID
            var _ShopID = Products.ShopID;
            Shops = await _db.Shops.FindAsync(_ShopID);
        }
    }
}
