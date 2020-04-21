using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Product> Products { get; set; }

        public async Task OnGet()
        {
            Products = await _db.Products.ToListAsync();
        }
    }
}
