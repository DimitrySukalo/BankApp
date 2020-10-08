using BankApp.BLL.Interfaces;
using BankApp.BLL.Services;
using BankApp.DAL.Entities;
using BankApp.DAL.Entity;
using BankApp.DAL.Interfaces;
using BankApp.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankApp.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Connection string to the database
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            //Connecting to the database
            services.AddDbContext<BankContext>(options => options.UseSqlServer(connectionString));

            //Adding identity
            services.AddIdentity<User, IdentityRole>(opt => { 
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<BankContext>().AddDefaultTokenProviders();

            //Adding register service for registration ability
            services.AddTransient<IRegisterService, RegisterService>();

            //Adding login service
            services.AddTransient<ILoginService, LoginService>();

            //Adding email service
            services.AddTransient<ISendEmailService, SendEmailService>();

            //Adding home service
            services.AddTransient<IHomeService, HomeService>();

            //Adding wallet service
            services.AddTransient<IWalletService, WalletService>();

            //Adding setting service
            services.AddTransient<ISettingService, SettingService>();

            //Adding piggy bank service
            services.AddTransient<IPiggyBankService, PiggyBankService>();

            //Adding unit of work
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddControllersWithViews();
        }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
