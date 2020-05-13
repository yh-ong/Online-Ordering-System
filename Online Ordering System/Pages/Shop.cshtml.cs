using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Online_Ordering_System.Extensions;
using Online_Ordering_System.Model;

namespace Online_Ordering_System.Pages
{
    public class ShopModel : PageModel
    {
        private ApplicationDbContext _db;

        public ShopModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public class InputModel
        {
            public int ProductID { get; set; }
            public string CurrentURL { get; set; }
        }

        public class ResponseModel
        {
            public string RedirectURL { get; set; }
            public string Status { get; set; }
        }

        public InputModel Input { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public Shop Shops { get; set; }

        [BindProperty]
        public Cart Carts { get; set; }

        public async Task OnGet(int id)
        {
            Shops = await _db.Shops.FindAsync(id);
            // Get the product's ShopID
            var _ShopID = Shops.ShopID;
            Products = await _db.Products.Where(p => p.ShopID == _ShopID).ToListAsync();
        }

        public IActionResult OnPost([FromBody] InputModel model)
        {
            try
            {
                int userID = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "IdentityID").Value);
                var existProduct = _db.Carts.SingleOrDefault(e => e.ProductID == model.ProductID && e.UserID == userID);
                if (existProduct == null)
                {
                    Carts.UserID = userID;
                    Carts.ProductID = model.ProductID;
                    Carts.QtyCart = "1";
                    Carts.DateAdded = DateTime.Now;

                    _db.Add(Carts);
                    _db.SaveChanges();
                }

                ResponseModel ob = new ResponseModel();
                ob.Status = "success";
                ob.RedirectURL = null;
                return new JsonResult(ob);
            }
            catch (Exception e)
            {
                ResponseModel ob = new ResponseModel();
                ob.Status = e.Message;
                ob.RedirectURL = "/SignIn?ReturnUrl=%2F" + model.CurrentURL.Replace("/", "");
                return new JsonResult(ob);
            }
        }
    }
}
