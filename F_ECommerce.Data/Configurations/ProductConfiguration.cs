using F_ECommerce.Core.Models.ProductModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace F_ECommerce.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
   public void Configure(EntityTypeBuilder<Product> builder)
   {
      builder.Property(x => x.Name).IsRequired();
      builder.Property(x => x.Description).IsRequired();
      builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)");
      builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");
      builder.HasData(
          new Product { Id = 1, Name = "test", Description = "test", CategoryId = 1, NewPrice = 12 });
   }
}
