using F_ECommerce.Core.Models.BaseModels;

namespace F_ECommerce.Core.Models.OrderModels;

public class OrderItem : BaseEntity
{
   public OrderItem()
   {

   }
   public OrderItem(long productItemId, string mainImage, string productName, decimal price, int quntity)
   {
      ProductItemId = productItemId;
      MainImage = mainImage;
      ProductName = productName;
      Price = price;
      Quntity = quntity;
   }

   public long ProductItemId { get; set; }
   public string MainImage { get; set; }
   public string ProductName { get; set; }
   public decimal Price { get; set; }
   public int Quntity { get; set; }

}
