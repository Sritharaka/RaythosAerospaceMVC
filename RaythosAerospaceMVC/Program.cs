//using Microsoft.Extensions.Configuration;
//using RaythosAerospaceMVC.Models;
//using Microsoft.EntityFrameworkCore;
//using RaythosAerospaceMVC.Repository;
//using Microsoft.Data.SqlClient;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<AerospaceDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IAircraftRepository, AircraftRepository>();



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//// Inside ConfigureServices method in Startup.cs
//void ConfigureServices(IServiceCollection services)
//{
//    // Other services configuration

//    services.AddSession(options =>
//    {
//        options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout if needed
//        // Additional session configurations can be added here
//    });

//    // Other configurations
//}


//void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//{
//    // Other app configurations

//    app.UseRouting();

//    app.UseSession(); // Place this before UseAuthorization()

//    app.UseAuthorization();

//    // Other middleware configurations
//}


////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Home}/{action=Index}/{id?}"
////    );

////app.UseEndpoints(endpoints =>
////{
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=User}/{action=Login}/{id?}"
////defaults: new { controller = "User", action = "Login" }
//);
////});

//app.MapControllerRoute(
//    name: "AircraftDetails",
//    pattern: "Aircraft/Details/{id}",
//    defaults: new { controller = "AircraftController", action = "Details" }
//);



//app.Run();


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RaythosAerospaceMVC;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
