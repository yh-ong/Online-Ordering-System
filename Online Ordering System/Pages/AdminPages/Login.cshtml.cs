using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages
{
    public class LoginModel : PageModel
    {
        private ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Seller Sellers { get; set; }

        [BindProperty] [Required]
        public string inputEmail { get; set; }

        [BindProperty] [Required]
        public string inputPassword { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult onPost()
        {
            if (ModelState.IsValid)
            {
                var UserQuery = _db.Sellers.FirstOrDefault(x => x.SellerEmailAddress == inputEmail && x.SellerPassword == inputPassword);

                if (UserQuery != null)
                {
                    HttpContext.Session.SetString("AdminLoggedIn", "1");
                    HttpContext.Session.SetString("SessionAdminEmail", inputEmail);
                    return RedirectToPage("Dashboard");
                }
                else
                {
                    Message = "Invalid Email & Password";
                    return Page();
                }
            } 
            else
            {
                return Page();
            }
        }
    }
}
