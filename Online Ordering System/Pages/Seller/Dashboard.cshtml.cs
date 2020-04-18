using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages.Seller
{
    public class DashboardModel : PageModel
    {
        private ApplicationDbContext _db;

        public DashboardModel(ApplicationDbContext db)
        {
            _db = db;
        }

        
        public void OnGet()
        {
            /*
            try
            {
                if (!this.User.Identity.IsAuthenticated)
                {
                    this.RedirectToPage("/Login");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            this.Page(); 
            */
        }
    }
}
