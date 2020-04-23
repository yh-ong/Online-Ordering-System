using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Shop Shops { get; set; }

        public User Users { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var IdentityEmail = User.Claims.Where(c => c.Type == ClaimTypes.Email)
                        .Select(c => c.Value).SingleOrDefault();
                    int IdentityID = Int32.Parse(User.Claims.First(c => c.Type.Equals("IdentityID")).Value);

                    Shops.UserID = IdentityID;
                    Shops.DateAdded = DateTime.Now;

                    await _db.Shops.AddAsync(Shops);

                    /* Update the database */
                    var dbUser = _db.Users.FirstOrDefault(u => u.UserID.Equals(IdentityID));
                    dbUser.Role = "Admin";

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    /* Update the claim data */
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, IdentityEmail),
                        new Claim("IdentityID", IdentityID.ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    await _db.SaveChangesAsync();
                    return RedirectToPage("Dashboard");
                }
                else
                {
                    return RedirectToPage("/SignIn");
                }
            }
            else
            {
                return Page();
            }
        }

    }
}
