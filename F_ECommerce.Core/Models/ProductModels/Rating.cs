using F_ECommerce.Core.Models.BaseModels;
using F_ECommerce.Core.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F_ECommerce.Core.Models.ProductModels;

public class Rating : BaseEntity
{
   [Range(1, 5)]
   public int Stars { get; set; }
   public string content { get; set; }
   public DateTime Review { get; set; } = DateTime.Now;
   public string AppUserId { get; set; }
   [ForeignKey(nameof(AppUserId))]
   public virtual AppUser AppUser { get; set; }

   public int ProductId { get; set; }
   [ForeignKey(nameof(ProductId))]
   public virtual Product Product { get; set; }
}
