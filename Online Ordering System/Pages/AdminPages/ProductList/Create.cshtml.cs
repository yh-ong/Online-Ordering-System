using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Online_Ordering_System.Extensions;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages.ProductList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostEnvironment _env;

        public CreateModel(ApplicationDbContext db, IHostEnvironment env)
        {
            _db = db;
            _env = env;

            this.CategoryList();
        }

        [BindProperty]
        public Product Products { get; set; }

        [BindProperty]
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please choose image")]
        public IFormFile ProductImage { get; set; }

        public List<SelectListItem> Options { get; set; }

        public void CategoryList()
        {
            Options = _db.Categories
                .Select(c => new SelectListItem { Value = c.CategoryID.ToString(), Text = c.Name })
                .ToList();

            Options.Insert(0, new SelectListItem { Value = "", Text = "Choose" });
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                int User_ID = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "IdentityID").Value);
                int Shop_ID = (from s in _db.Shops where s.UserID == User_ID select s.ShopID).FirstOrDefault();

                var folderPath = "Uploads/" + Shop_ID;

                string uploaded_name = null;
                string extension = Path.GetExtension(ProductImage.FileName);
                if (UploadExtension.IsImgExtension(extension))
                {
                    uploaded_name = UploadImage(ProductImage, folderPath);
                }
                else
                {
                    ModelState.AddModelError(nameof(ProductImage), "Invalid File Format. Only Accept: PNG, JPEG, JPG");
                    return Page();
                }

                Products.ShopID = Shop_ID;
                Products.Image = uploaded_name;

                await _db.Products.AddAsync(Products);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public string UploadImage(IFormFile inputFilename, string inputFolderpath)
        {
            if (!Directory.Exists(inputFolderpath))
            {
                Directory.CreateDirectory(inputFolderpath);
            }

            string uploadFile = null;
            if (inputFilename != null)
            {
                string uploadFolder = Path.Combine(_env.ContentRootPath, inputFolderpath);
                uploadFile = Guid.NewGuid().ToString() + "_" + inputFilename.FileName;

                string filePath = Path.Combine(uploadFolder, uploadFile);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    inputFilename.CopyTo(fileStream);
                }
            }

            return uploadFile;
        }

    }
}
