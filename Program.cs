using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VirvisShopFinal.Context;
using VirvisShopFinal.Models;

namespace VirvisShopFinal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<VirvisDatabaseContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionStrings:VirvisShopDBConnection"]));


            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Users/Login"; // Ruta para el login si no está autenticado
            //    options.LogoutPath = "/Users/Logout"; // Ruta para hacer logout
            //    options.AccessDeniedPath = "/Users/AccessDenied"; // Ruta si el usuario no tiene acceso
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
            //});

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
                options.Cookie.HttpOnly = true; // Mayor seguridad
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
                       
            
            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
