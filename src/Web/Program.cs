using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memiarzeEu.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace memiarzeEu
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await SeedRoleData(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static async Task SeedRoleData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
                }

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }

                if (userManager.FindByNameAsync("admin").Result == null)
                {
                    var user = new ApplicationUser { UserName = "admin", AvatarPath = configuration.GetSection("DefaultAvatarPath").Value };
                    await userManager.CreateAsync(user, "admin123");
                    await userManager.AddToRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}