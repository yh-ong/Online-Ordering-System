using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public void OnGet()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "1")
            {
                Response.Redirect("/AdminPages/Login");
            }
            else
            {
                Redirect("/AdminPages/Dashbard");
            }
        }
    }
}
