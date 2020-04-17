using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.OleDb;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages
{
    public class CartModel : PageModel
    {

        public IEnumerable<Cart> Carts { get; set; }
        public void OnGet()
        {
        }
    }
}
