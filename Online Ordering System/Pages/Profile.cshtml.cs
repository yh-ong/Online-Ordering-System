using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages
{
    public class ProfileModel : PageModel
    {
        private ApplicationDbContext _db;
        
        public ProfileModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public User Users { get; set; }

        public int IdentityID()
        {
            return Int32.Parse(User.Claims.First(c => c.Type.Equals("IdentityID")).Value);
        }

        public async Task OnGetAsync()
        {
            Users = await _db.Users.FindAsync(IdentityID());
        }

        public async Task<IActionResult> OnPostPersonalAsync()
        {
            if(!(String.IsNullOrEmpty(Users.FirstName)) && !(String.IsNullOrEmpty(Users.LastName)))
            {
                var dbUser = await _db.Users.FindAsync(IdentityID());
                dbUser.FirstName = Users.FirstName;
                dbUser.LastName = Users.LastName;
                dbUser.PhoneNumber = Users.PhoneNumber;

                await _db.SaveChangesAsync();

                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAccountAsync()
        {
            if (ModelState.IsValid)
            {
                var OldPassword = (from User in _db.Users
                                  where User.UserID == IdentityID()
                                  select User.Password).FirstOrDefault();
                if (OldPassword.Equals(Users.Password) && Users.Password.Equals(Users.Password))
                {
                    var dbUser = await _db.Users.FindAsync(IdentityID());
                    dbUser.Email = Users.Email;
                    dbUser.Password = Users.Password;

                    await _db.SaveChangesAsync();
                    return RedirectToPage();
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostAddressAsync()
        {
            var dbUser = await _db.Users.FindAsync(IdentityID());
            dbUser.Address = Users.Address;
            dbUser.State = Users.State;
            dbUser.City = Users.City;
            dbUser.PostalCode = Users.PostalCode;
            dbUser.Country = Users.Country;

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

    }
}
