using Microsoft.AspNetCore.Http;

namespace F_ECommerce.Core.ViewModels.ProductVMs;

public record AddProductVM
{
   public string Name { get; set; }
   public string Description { get; set; }
   public decimal NewPrice { get; set; }
   public decimal OldPrice { get; set; }
   public int CategoryId { get; set; }
   public IFormFileCollection Photo { get; set; }
}
