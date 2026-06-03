using ECommerce.BLL;
using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDALServices(builder.Configuration);
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                //options.Lockout.MaxFailedAccessAttempts = 3;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                //options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.RequireUniqueEmail = false;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<EComAppDbContext>();
            builder.Services.AddDBLLServices();

            var app = builder.Build();

            #region SeedRoles
            // First Roles And Admin User
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                string[] roles = { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = role });
                    }
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminEmail = "admin@gmail.com";
                string password = "1234";

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new ApplicationUser
                    {
                        FirstName = "Mohamed",
                        LastName = "Saad",
                        UserName = "MoSaad",
                        Email = adminEmail
                    };

                    await userManager.CreateAsync(admin, password);
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
