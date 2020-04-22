using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Ordering_System.Model;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Online_Ordering_System.Extensions;

namespace Online_Ordering_System.Pages
{
    public class SignInModel : PageModel
    {
        private ApplicationDbContext _db;

        public SignInModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public User Users { get; set; }

        public Shop Shops { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public string Message { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public string ReturnUrl { get; private set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ReturnUrl = returnUrl;
                var res = await AuthenticateUser(Input.Email, Input.Password);

                if (res == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, res.Email),
                    new Claim("IdentityID", res.UserID.ToString()),
                    new Claim(ClaimTypes.Role, res.Role)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return LocalRedirect(Url.GetLocalUrl(returnUrl));
            }

            // Something failed. Redisplay the form.
            return Page();
        }

        private async Task<User> AuthenticateUser(string email, string password)
        {
            await Task.Delay(500);

            var UserQuery = _db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            //if (string.Equals(email, "maria.rodriguez@contoso.com", StringComparison.OrdinalIgnoreCase))
            if (UserQuery != null)
            {
                var SelectQuery = (from User in _db.Users
                                  where User.Email == email
                                  select User.UserID).FirstOrDefault();

                // Role-Based Policy
                var UserRole = (from User in _db.Users
                                    where User.UserID == SelectQuery
                                    select User.Role).FirstOrDefault();

                return new User()
                {
                    Email = email,
                    UserID = SelectQuery,
                    Role = UserRole
                };
            }
            else
            {
                return null;
            }
        }

        // First Use I implemented in this, However i found out the best way in .net for authentication
        /*
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var UserQuery = _db.Users.FirstOrDefault(x => x.Email == inputEmail && x.Password == inputPassword);

                if (UserQuery != null)
                {
                    HttpContext.Session.SetString("LoggedIn", "true");
                    HttpContext.Session.SetString("SessionEmail", inputEmail);

                    return RedirectToPage("/Index");
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
        */

    }
}
