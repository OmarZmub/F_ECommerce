using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using F_ECommerce.Core.Models.OrderModels;

namespace F_ECommerce.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
   public void Configure(EntityTypeBuilder<Order> builder)
   {
      builder.OwnsOne(x => x.ShippingAddress,
          n => { n.WithOwner(); });

      builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

      builder.Property(x => x.Status).HasConversion(o => o.ToString(),
          o => (Status) Enum.Parse(typeof(Status), o));

      builder.Property(m => m.SubTotal).HasColumnType("decimal(18,2)");
   }
}
