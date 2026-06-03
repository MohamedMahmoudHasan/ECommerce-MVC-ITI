using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DAL
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.HasData(
                new Category { Id = 1, Name = "Electronics", CreatedAt = new DateTime(2026, 6, 1) },
                new Category { Id = 2, Name = "Accessories", CreatedAt = new DateTime(2026, 6, 1) }
            );


        }
    }
}
