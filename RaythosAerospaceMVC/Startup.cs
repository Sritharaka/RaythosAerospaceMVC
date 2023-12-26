using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.EntityFrameworkCore;
using RaythosAerospaceMVC.Models;
using RaythosAerospaceMVC.Repository;
using System.Net.Mail;
using RaythosAerospaceMVC.Repositories;

namespace RaythosAerospaceMVC
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
            services.AddControllersWithViews();

            services.AddDbContext<AerospaceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout if needed
                                                                // Additional session configurations can be added here
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAircraftRepository, AircraftRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IManufacturingProgressRepository, ManufacturingProgressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddScoped<SmtpClient>();




            // Other service configurations
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}"
                //);

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}"
                //defaults: new { controller = "User", action = "Login" }
                );

                endpoints.MapControllerRoute(
                    name: "AircraftDetails",
                    pattern: "Aircraft/Details/{id}",
                    defaults: new { controller = "Aircraft", action = "Details" }
                );
            });
        }
    }


}
