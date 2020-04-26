using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Ordering_System.Extensions;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages.ProductList
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Shop Shops { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public async Task OnGet()
        {
            int _UserID = Int32.Parse(User.GetClaimValue("IdentityID"));
            var _ShopID = (from s in _db.Shops
                           where s.UserID == _UserID
                           select s.ShopID).FirstOrDefault();

            /* List all of the products without primary key */
            //Products = await _db.Products.ToListAsync();
            Products = await _db.Products.Where(p => p.ShopID.Equals(_ShopID)).ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Product = await _db.Products.FindAsync(id);
            if (Product != null)
            {
                return NotFound();
            }
            else
            {
                _db.Products.Remove(Product);
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
        }
    }
}
