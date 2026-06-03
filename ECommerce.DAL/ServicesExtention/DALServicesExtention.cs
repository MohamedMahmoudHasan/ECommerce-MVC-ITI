using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL
{
    public static class DALServicesExtention
    {
        public static void AddDALServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<EComAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            

            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
