using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Yan.AdminUI2
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddMemoryCache();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromDays(6);
                options.Cookie.HttpOnly = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Login/Index");
                    options.LogoutPath = new PathString("/Login/Logout");
                    options.AccessDeniedPath = new PathString("/Login/Denied");

                    options.Events.OnRedirectToLogin = context =>
                    {
                        if(context.Request.Headers.ContainsKey("axios") && context.Request.Headers["axios"]=="true")
                        {
                            context.Response.StatusCode = 401;
                        }
                        else
                        {
                            context.Response.Redirect(context.RedirectUri);
                        }
                        return Task.CompletedTask;
                    };

                    options.Cookie.Name = "_YY.AdminUI";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
                    options.SlidingExpiration = false;
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
