using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vancil.App;
using vancil.Framework.Account;
using vancil.Framework.Helpers.DatabaseHelper;
using vancil.Models;

namespace vancil
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Pull in scopes, singletons, transients from config
            new ServicesConfiguration().SetServices(services);
            
            //Handle Databases
            var databaseType = Configuration.GetSection("Database:DatabaseType");
            
            services.AddDbContext<AppDbContext>(ConfigureMySQLServer);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = HostingEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.Name = "Vancil.Cookies";
            });

            services.Configure<CookiePolicyOptions>(options =>{
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = HostingEnvironment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
            });

            services.AddControllersWithViews();

            // Sets the option to not allow anonymouse users. Will always send to login page if [AllowAnonymous] is not used on the method
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureMySQLServer(DbContextOptionsBuilder options)
        {
            var connectionStringHelper = new ConnectionStringHelper();
            var connectionString = connectionStringHelper.CreateConnectionString(Configuration);

            options.UseMySql(connectionString);
        }
    }
}
