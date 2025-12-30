using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Product Configurations. 

            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(200);

            builder.Property(p => p.PictureUrl)
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(300);

            // Relation ship configurations

            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.NoAction); // To Delete brand without deleting it's products


            builder.HasOne(p => p.Type)
                .WithMany()
                .HasForeignKey(P => P.TypeId)
                .OnDelete(DeleteBehavior.NoAction); // To Delete Type without  deleting it's products.

        }
    }
}
