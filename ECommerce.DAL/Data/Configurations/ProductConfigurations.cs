using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace ECommerce.DAL
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(p => p.Title)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasData(
                new Product { Id = 1, Title = "Laptop", Description = "A high-performance laptop", Price = 999.99m ,Count = 2 , CategoryId = 1 , CreatedAt = new DateTime(2026, 6, 1) },
                new Product { Id = 2, Title = "Smartphone", Description = "A latest model smartphone", Price = 499.99m, Count = 3, CategoryId = 1, CreatedAt = new DateTime(2026, 6, 1) },
                new Product { Id = 3, Title = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, Count = 4, CategoryId = 2, CreatedAt = new DateTime(2026, 6, 1) }
            );


        }
    }
}
