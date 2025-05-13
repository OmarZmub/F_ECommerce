using F_ECommerce.Core.Models.ProductModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace F_ECommerce.Data.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
   public void Configure(EntityTypeBuilder<Photo> builder)
   {
      builder.HasData(new Photo { Id = 3, ImageName = "test", ProductId = 1 });
   }
}
