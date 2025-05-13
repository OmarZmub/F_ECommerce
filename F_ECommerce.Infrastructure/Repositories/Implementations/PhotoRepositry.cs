using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using F_ECommerce.Infrastructure.Repositories.Implementations.Generic;

namespace F_ECommerce.Infrastructure.Repositories.Implementations;

public class PhotoRepositry : GenericRepositry<Photo>, IPhotoRepositry
{
   public PhotoRepositry(AppDbContext context) : base(context)
   {
   }
}
