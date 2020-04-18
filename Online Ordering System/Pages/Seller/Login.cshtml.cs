using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Online_Ordering_System.Pages.Seller
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            Response.Redirect("/Seller/Dashboard");
        }
    }
}
