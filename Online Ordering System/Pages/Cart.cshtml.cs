using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Online_Ordering_System.Model;
using Microsoft.EntityFrameworkCore;

namespace Online_Ordering_System.Pages
{
    public class CartModel : PageModel
    {
        private ApplicationDbContext _db;

        public CartModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Cart> Carts { get; set; }
        public async Task OnGet()
        {
            Carts = await _db.Carts.ToListAsync();
        }
    }
}
