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
            //підключення конфіг з appsetting.json
            //Configuration.Bind("Progect", new Config());



            //Підключення функціоналу проекту в якості сервісів
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemRepository>();
                        services.AddTransient<DataManager>();

            

            //Підключення контексту БД
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            //Ідентіті система(налаштування)

            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Налаштування authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;

            });

            //Добавлення підтримки контролерів і уявлень (MVC)
            services.AddControllersWithViews()
            //Сумісність  з app.net core 3.0
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Порядок реєстрації middleware важливий
            //Треба бачити помилки під час розробки
            if (env.IsDevelopment())app.UseDeveloperExceptionPage();

            //Підключення статичних файлів (хтмл, Js, css)
            app.UseStaticFiles();

            //Система маршрутизації (підключення)
            app.UseRouting();

            //Підключення аутентифікації і авторизації
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //Реєстрація потрібних маршрутів
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
