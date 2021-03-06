﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Online_Ordering_System.Model;
using Online_Ordering_System.Extensions;

namespace Online_Ordering_System.Pages
{
    public class IndexModel : PageModel
    {
        /*
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        } */

        public ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> Categories { get; set; }

        public string BannerTitle { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                //ID = User.Claims.First(c => c.Type.Equals("IdentityID", StringComparison.OrdinalIgnoreCase)).Value;
                BannerTitle = "Welcome to Online Buy";
            } 
            else
            {
                BannerTitle = "Sign Up for Online Buy";
            }

            Categories = await _db.Categories.ToListAsync();
        }
    }
}
