using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages.ProductList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product Products { get; set; }

        public List<SelectListItem> Options { get; set; }

        public async Task OnGet()
        {
            Options = await _db.Categories
                .Select(c => new SelectListItem { Value = c.CategoryID.ToString(), Text = c.Name })
                .ToListAsync();

            Options.Insert(0, new SelectListItem { Value = "", Text = "Choose" });
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                
                string uploadFilename = null;
                if (uploadFilename != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uploadFilename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Products.Image);

                    string filePath = Path.Combine(uploadFolder, uploadFilename);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //Products.Image.CopyTo(fileStream);
                    }
                }

                await _db.Products.AddAsync(Products);
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
