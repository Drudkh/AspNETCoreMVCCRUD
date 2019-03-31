using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.netCoreMVCCRUD.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asp.netCoreMVCCRUD
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

                     
        // Слід використовувати цей метод для додавання сервісів до контейнерів
        public void ConfigureServices(IServiceCollection services) //реєстрація сервісів ( внедрения зависимостей (DI))
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true; // true означает, что у пользователя будет запращиваться согласие на установку кук.
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<EmployeeContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection_01")));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) //якщо в процесі розробки
            {
                app.UseDeveloperExceptionPage(); // вивід інформації про помилку
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); 
            }

            app.UseStaticFiles();//помічає файли в кореневому веб-каталозі як обслуговувані
            app.UseCookiePolicy();// CookiePolicyMiddleware обробникдля роботи з кукы
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Employee}/{action=Index}/{id?}");
            });
        }


    }
}
