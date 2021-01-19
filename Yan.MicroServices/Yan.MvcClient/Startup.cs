using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yan.MvcClient.Clients;
using Yan.MvcClient.Message;

namespace Yan.MvcClient
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); //cshtml �������֮�� ������������ ������Ч

            # region SignalR
            services.AddSignalR(options =>
            {
                //ʵʱͨ�ţ���Ҫ��������������˫������Ҫȷ���Է����ڲ��ڣ����ҵ��Ļ��Һ��������߰���ɵ�����
                //���Ծ���������������һ���Ƿ��������ļ��ʱ�䣬��һ�����ǵȴ��Է�����������ȴ�ʱ�䡣
                //һ��ȴ���ʱ�����óɷ��������ļ��ʱ�����������
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });
            #endregion

            #region ����

            services.AddCors(options =>
            {
                options.AddPolicy("AnyCors",
                    policy =>
                    {
                        policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });

            #endregion

            #region ҵ�����ע��

            services.AddHttpClient();
            services.AddHttpClient<ArticleServiceClient>(client =>
            {
                //client.BaseAddress = new Uri("http://118.24.205.200:5000");
                client.BaseAddress = new Uri("http://localhost:5000");
            });
            services.AddHttpClient<BillClient>(client =>
            {
                //client.BaseAddress = new Uri("http://118.24.205.200:5000");
                client.BaseAddress = new Uri("http://localhost:5000");
            });

            #endregion

            #region ��֤
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => { })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.Authority = "http://118.24.205.200:5100";
                    options.Authority = "http://localhost:5100";
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "Yan.MvcClient";
                    options.ClientSecret = "Yan.MvcClient";
                    options.SaveTokens = true;
                    options.ResponseType = "code";
                    options.Scope.Clear();
                    options.Scope.Add("system");
                    options.Scope.Add("article");
                    options.Scope.Add(OidcConstants.StandardScopes.OpenId);
                    options.Scope.Add(OidcConstants.StandardScopes.Profile);
                    options.Scope.Add(OidcConstants.StandardScopes.Address);
                    options.Scope.Add(OidcConstants.StandardScopes.Email);
                    options.Scope.Add(OidcConstants.StandardScopes.Phone);
                    // ��identity server��AllowOfflineAccess=true,��Ӧ��offline_access��ָ�����ܷ���refreshtoken������������
                    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);

                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:5100";
                    options.Audience = "article";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            if (!context.HttpContext.Request.Path.HasValue)
                            {
                                return Task.CompletedTask;
                            }

                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!(string.IsNullOrEmpty(accessToken)) && path.StartsWithSegments("/messagehub"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            #region ע��IHttpContextAccessor,�����ȡHttpContext
            services.AddHttpContextAccessor();
            #endregion

            services.AddAuthorization();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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

            app.UseCors("AnyCors");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapWhen(x => x.Request.Path.StartsWithSegments("/api/articlemanage/src/Pictures"), builder =>
            {
                builder.Run( async context =>
                {
                    context.Response.Redirect("http://118.24.205.200:5000" + context.Request.Path);
                });
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets();//���WebSockets֧�֣�SignalR����ʹ��WebSocket����

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<MessageHub>("/messagehub");
            });
        }
    }
}
