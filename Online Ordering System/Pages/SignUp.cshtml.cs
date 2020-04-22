using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Users.Role = "User";
                await _db.Users.AddAsync(Users);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
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
