using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using F_ECommerce.Infrastructure.Repositories.Implementations.Generic;

namespace F_ECommerce.Infrastructure.Repositories.Implementations;

public class CategoryRepositry : GenericRepositry<Category>, ICategoryRepositry
{
   public CategoryRepositry(AppDbContext context) : base(context)
   {
   }
}
