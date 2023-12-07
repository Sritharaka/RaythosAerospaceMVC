using Microsoft.Extensions.Configuration;
using RaythosAerospaceMVC.Models;
using Microsoft.EntityFrameworkCore;
using RaythosAerospaceMVC.Repository;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AerospaceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();


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



app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//    );

//app.UseEndpoints(endpoints =>
//{
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}"
//defaults: new { controller = "User", action = "Login" }
);
//});


app.Run();
