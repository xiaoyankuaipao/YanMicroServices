using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yan.Idp.Models;
using Serilog;

namespace Yan.Idp.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;
            });

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var test = userMgr.FindByNameAsync("test").Result;
                    if (test == null)
                    {
                        test = new ApplicationUser
                        {
                            UserName = "test",
                            Email = "511657675@qq.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(test, "test").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(test, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "测试人员"),
                            new Claim(JwtClaimTypes.GivenName, "Test"),
                            new Claim(JwtClaimTypes.FamilyName, "test"),
                            new Claim(JwtClaimTypes.WebSite, "http://82.156.187.171:9090"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("test created");
                    }
                    else
                    {
                        Log.Debug("test already exists");
                    }

                    
                }
            }
        }
    }
}
