using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using memiarzeEu.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using memiarzeEu.Models;
using memiarzeEu.Interfaces;
using memiarzeEu.Services;
using memiarzeEu.Validation;
using Microsoft.AspNetCore.Http.Features;

namespace memiarzeEu
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
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IMemeFileService, LocalMemeFileService>();
            services.AddScoped<IAvatarFileService, LocalAvatarFileService>();
            services.AddSingleton(typeof(IXdPointFactory<>), typeof(XdPointFactory<>));

            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                PasswordOptionsStore.IdentityOptions = options;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 512000;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{page?}");
            });
        }
    }
}
