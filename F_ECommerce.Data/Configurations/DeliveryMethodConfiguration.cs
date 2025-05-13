using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using F_ECommerce.Core.Models.OrderModels;

namespace F_ECommerce.Data.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
   public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
   {
      builder.Property(m => m.Price).HasColumnType("decimal(18,2)");
      builder.HasData(
  new DeliveryMethod { Id = 1, DeliveryTime = "Only a week", Description = "The fast Delivery in the world", Name = "DHL", Price = 15 },
  new DeliveryMethod { Id = 2, DeliveryTime = "Only take two week", Description = "Make your product save", Name = "XXX", Price = 12 }
      );
   }
}
