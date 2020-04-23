using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public SignUpModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public User Users { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Input.ConfirmPassword != null)
                {
                    if (Users.Password == Input.ConfirmPassword)
                    {
                        Users.Role = "User";
                        await _db.Users.AddAsync(Users);
                        await _db.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        ErrorMessage = "Confirm password doesn't match!";
                        return Page();
                    }
                }
                else
                {
                    ErrorMessage = "The Confirm Password field is required.";
                    return Page();
                }
            } 
            else
            {
                return Page();
            }
        }
        
        public void OnGet()
        {
        }
    }
}
