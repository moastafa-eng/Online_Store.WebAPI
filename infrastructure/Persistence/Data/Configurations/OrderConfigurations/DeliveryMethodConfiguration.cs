using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.OrderConfigurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.ShortName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(D => D.DeliveyTime).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Cost).HasColumnType("decimal(18, 2)");
        }
    }
}
