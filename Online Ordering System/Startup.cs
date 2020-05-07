using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Online_Ordering_System.Extensions;
using Online_Ordering_System.Model;

namespace Online_Ordering_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the access Database on appsetting.json file
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Session Service
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // Authorization and Authentication Service
            // Determine wheather non-essential cookie request
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            // Return to the Sign In (URL) If user is not authenticate
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(configs =>
            {
                configs.LoginPath = "/SignIn"; // If not authenticate
                configs.AccessDeniedPath = "/AdminPages/Register"; // If policy not qualified
            });

            // Apply Policy
            services.AddAuthorization(configs =>
            {
                configs.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            services.AddRazorPages()
                // Authorize person Who can access
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/AdminPages/ProductList", "RequireAdminRole");
                    options.Conventions.AuthorizePage("/AdminPages/Dashboard", "RequireAdminRole");
                    options.Conventions.AuthorizePage("/Profile");
                    options.Conventions.AuthorizePage("/Cart");
                    options.Conventions.AllowAnonymousToPage("/Index");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });

            // Session for Authentication
            app.UseSession();

            app.UseRouting();

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
