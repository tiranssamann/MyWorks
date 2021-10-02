using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StalkerShop.Data.Interfaces;
using StalkerShop.Data.Models;
using StalkerShop.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using StalkerShop.Data.Repository;
using Microsoft.AspNetCore.Mvc;
namespace StalkerShop
{
    public class Startup
    {
        private IConfigurationRoot _confscring;
        public Startup(IHostingEnvironment hostenv)
        {
            _confscring = new ConfigurationBuilder().SetBasePath(hostenv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confscring.GetConnectionString("DefaultConnection")));
            //связка интерфейса с классом
            services.AddTransient<IAllOrders, OrdersRepository>();
            services.AddTransient<IAllRadioItems, RadioItemsRepository>();
            services.AddTransient<IRadioCategory, CategoryRepository>();
            // регистрация модулей
            //services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "categoryfilter", template: "RadioItems/{action}/{category?}", defaults: new { Controller = "RadioItems", action = "List" });
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
        }
    }
}
