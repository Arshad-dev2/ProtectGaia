using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ProtectGaia.DataAccess;
using ProtectGaia.Interface;
using Microsoft.EntityFrameworkCore;
using ProtectGaia.DataContexts;
using ProtectGaia.Interfaces;
using ProtectGaia.Implementations;

namespace ProtectGaia
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


            services.AddControllersWithViews();

            services.AddDbContextPool<ChallengeDB>(options => options.UseSqlServer(Configuration.GetConnectionString("EcoMorphConnection")));

            //services.AddDbContextPool<ChallengeDB>(options => options.UseSqlServer(Configuration.GetConnectionString("EcoMorphConnection")));
            //services.AddDbContextPool<ChallengeDB>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("EcoMorphConnection")));

            services.AddDistributedMemoryCache();


            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddScoped<INewsApi, NewsApi>();
            services.AddScoped<IWeatherApi, WeatherApi>();
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IChallenge, ChallengeRepository >();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
