using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCompany
{
    public class Startup
    {
    public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;


        public void ConfigureServices(IServiceCollection services)
        {
            //���������� ������ � appsetting.json
            //Configuration.Bind("Progect", new Config());



            //ϳ��������� ����������� ������� � ����� ������
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemRepository>();
                        services.AddTransient<DataManager>();

            

            //ϳ��������� ��������� ��
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            //������ �������(������������)

            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //������������ authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;

            });

            //���������� �������� ���������� � ������� (MVC)
            services.AddControllersWithViews()
            //��������  � app.net core 3.0
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //������� ��������� middleware ��������
            //����� ������ ������� �� ��� ��������
            if (env.IsDevelopment())app.UseDeveloperExceptionPage();

            //ϳ��������� ��������� ����� (����, Js, css)
            app.UseStaticFiles();

            //������� ������������� (����������)
            app.UseRouting();

            //ϳ��������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //��������� �������� ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
