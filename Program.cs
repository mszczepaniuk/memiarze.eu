using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
                }

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }
            }
        }
    }
}
