namespace F_ECommerce.Core.ViewModels.OrderVMs;

public record OrderItemVM
{
   public int ProductItemId { get; set; }
   public string MainImage { get; set; }
   public string ProductName { get; set; }
   public decimal Price { get; set; }
   public int Quntity { get; set; }
}