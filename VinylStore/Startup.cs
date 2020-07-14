using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VinylStore.Models;
using System;

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
            services.AddDbContext<AppIdentityDbContext>(
                options => options.UseSqlServer(
                    Configuration["Data:VinylStore:ConnectionStrings:AppIdentityDbContext"])
                );
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddScoped<AppIdentityDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            { app.UseDeveloperExceptionPage(); }
            else
            { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
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
            IdentitySeedData.InitialBaseIdentityData(app);
        }
    }
}