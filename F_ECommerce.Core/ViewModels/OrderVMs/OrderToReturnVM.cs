using F_ECommerce.Core.Models.OrderModels;

namespace F_ECommerce.Core.ViewModels.OrderVMs;

public record OrderToReturnVM
{
   public int Id { get; set; }
   public string BuyerEmail { get; set; }
   public decimal SubTotal { get; set; }
   public decimal Total { get; set; }
   public DateTime OrderDate { get; set; }
   public ShippingAddress shippingAddress { get; set; }

   public IReadOnlyList<OrderItemVM> orderItems { get; set; }
   public string deliveryMethod { get; set; }


   public string status { get; set; }
}
