using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VinylStore.Models;

namespace VinylStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>(
                options => options.UseSqlServer(
                    Configuration["Data:VinylStore:ConnectionStrings:ProductDbContext"])
                    );
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{genre}/Page{page:int}",
                    defaults: new { controler = "Product", action = "List" }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page/{page:int}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{genre}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{controller=Product}/{action=List}/{id?}");
            });
            ProductSeedData.InitialBaseProductData(serviceProvider);
        }
    }
}
