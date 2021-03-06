using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages.ProductList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product Products { get; set; }

        public List<SelectListItem> Options { get; set; }

        public async Task OnGet(int id)
        {
            Products = await _db.Products.FindAsync(id);

            Options = await _db.Categories
                .Select(c => new SelectListItem { Value = c.CategoryID.ToString(), Text = c.Name })
                .ToListAsync();

            Options.Insert(0, new SelectListItem { Value = "", Text = "Choose" });
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var ProductDB = await _db.Products.FindAsync(Products.ProductID);
                ProductDB.Name = Products.Name;
                ProductDB.Description = Products.Description;
                ProductDB.CategoryID = Products.CategoryID;
                ProductDB.QtyInStock = Products.QtyInStock;
                ProductDB.Price = Products.Price;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

    }
}
