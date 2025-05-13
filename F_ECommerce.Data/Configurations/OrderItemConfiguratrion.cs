using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using F_ECommerce.Core.Models.OrderModels;

namespace F_ECommerce.Data.Configurations;

public class OrderItemConfiguratrion : IEntityTypeConfiguration<OrderItem>
{
   public void Configure(EntityTypeBuilder<OrderItem> builder)
   {
      builder.Property(m => m.Price).HasColumnType("decimal(18,2)");
   }
}
