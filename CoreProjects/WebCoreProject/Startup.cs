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
                //��¼·�������ǵ��û���ͼ������Դ��δ���������֤ʱ�����򽫻Ὣ�����ض���������·�� 
                o.LoginPath = new PathString("/Home/Login"); 
                //��ֹ����·�������û���ͼ������Դʱ����δͨ������Դ���κ���Ȩ���ԣ����󽫱��ض���������·���� 
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
            //NLog������
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


            app.UseAuthentication();//��֤//�����֤�м��



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
