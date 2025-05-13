using F_ECommerce.Core.Models.ProductModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_ECommerce.Core.ViewModels.OrderVMs;

namespace F_ECommerce.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
   public void Configure(EntityTypeBuilder<Category> builder)
   {
      builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
      builder.Property(x => x.Id).IsRequired();
      builder.HasData(
          new Category { Id = 1, Name = "test", Description = "test" }
          );
   }
}
