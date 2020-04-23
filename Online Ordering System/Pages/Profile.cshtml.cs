using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task OnGetAsync()
        {
            int IdentityID = Int32.Parse(User.Claims.First(c => c.Type.Equals("IdentityID")).Value);
            Users = await _db.Users.FindAsync(IdentityID);
        }

        public void OnPost()
        {
            int IdentityID = Int32.Parse(User.Claims.First(c => c.Type.Equals("IdentityID")).Value);
            var dbUser = _db.Users.FirstOrDefault(u => u.UserID.Equals(IdentityID));
        }
    }
}
