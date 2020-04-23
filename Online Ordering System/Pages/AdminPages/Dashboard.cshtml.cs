using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.AdminPages
{
    public class DashboardModel : PageModel
    {
        private ApplicationDbContext _db;

        public DashboardModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public User Users { get; set; }

        public Shop Shops { get; set; }

        public string canSell { get; set; }

        public void OnGet()
        {
            checkAuthenticate();
        }

        public void checkAuthenticate()
        {
            // Set Buyer to Seller Session
            // Check Entity 
            // HttpContext.Session.SetString("SellerLoggedIn", "true");

            var IdentityID = User.Claims.First(c => c.Type.Equals("IdentityID", StringComparison.OrdinalIgnoreCase)).Value;
            var ID = (from Shop in _db.Shops
                               where Shop.UserID.Equals(IdentityID)
                               select Shop.ShopID).FirstOrDefault().ToString();

            // if value = 1 (allowSell) ? (NotallowSell)
            //canSell = (from User in _db.Users
            //                    where User.UserID.Equals(IdentityID)
            //                    select User.CanSell).FirstOrDefault().ToString();

            //if (checkQuery != null)
            //{
            //    Response.Redirect("/AdminPages/Dashbard", false);
            //}
            //else
            //{
            //    Response.Redirect("/SignIn");
            //}
        }
    }
}
