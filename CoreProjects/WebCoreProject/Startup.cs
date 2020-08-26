using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Repository.impl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCoreProject.Models;

namespace WebCoreProject
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => { 
                //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径 
                o.LoginPath = new PathString("/Home/Login"); 
                //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。 
                o.AccessDeniedPath = new PathString("/Home/Index"); });

            services.AddControllersWithViews();
            services.AddDbContext<WebCoreProjectContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoreProjectContext")));
            services.AddMvc();

            services.AddHttpContextAccessor();

            WebCoreProjectContext.ConStr = Configuration.GetConnectionString("CoreProjectContext");
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //NLog的配置
            NLog.LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("CoreProjectContext");
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


            app.UseAuthentication();//认证//身份验证中间件



            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Shop}/{id?}");
            });
        }
    }
}
