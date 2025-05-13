using F_ECommerce.Core.Models.BaseModels;

namespace F_ECommerce.Core.Models.ProductModels;

public class Photo : BaseEntity
{
   public string ImageName { get; set; }

   public long ProductId { get; set; }
}
