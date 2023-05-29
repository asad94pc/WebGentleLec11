using Lec11.Data;
using Lec11.Models;
using Lec11.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Lec11
{
    public class Startup
    {
        IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddDbContext<BookDbContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;

            });


#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
              //uncomment the below code to disable client side validations  
            //.AddViewOptions(options =>
            //{
            //    options.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            
            services.Configure<BookAlertConfigModel>("MyBook1", _config.GetSection("MyNewBook"));
            services.Configure<BookAlertConfigModel>("MyBook2", _config.GetSection("ThridBook"));
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

            app.UseRouting();
            
            app.UseAuthentication();
            
            app.UseAuthorization();



            //app.MapRazorPages();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                //endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "bookApp/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
