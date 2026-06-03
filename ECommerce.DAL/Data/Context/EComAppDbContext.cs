using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL
{
    public class EComAppDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public EComAppDbContext(){}
        public EComAppDbContext(DbContextOptions<EComAppDbContext> options):base(options) {}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=. ;DataBase=EComAppDbLab2;Trusted_Connection = true;TrustServerCertificate = true");
        //}

        public override int SaveChanges()
        {
            AuditLog();
            return base.SaveChanges();
        }

        private void AuditLog()
        {
            var dateNow = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = dateNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = dateNow;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Product>(new ProductConfigurations());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfigurations());
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
