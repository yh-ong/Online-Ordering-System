using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Online_Ordering_System.Model;
using Microsoft.EntityFrameworkCore;

namespace Online_Ordering_System.Pages
{
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public ListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Shop> Shops { get; set; }

        public async Task OnGet()
        {
            Categories = await _db.Categories.ToListAsync();
            Shops = await _db.Shops.ToListAsync();
        }
    }
}
