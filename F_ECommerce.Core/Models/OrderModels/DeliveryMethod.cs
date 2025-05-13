using F_ECommerce.Core.Models.BaseModels;

namespace F_ECommerce.Core.Models.OrderModels;

public class DeliveryMethod : BaseEntity
{
   public DeliveryMethod()
   {

   }
   public DeliveryMethod(string name, decimal price, string deliveryTime, string description)
   {
      Name = name;
      Price = price;
      DeliveryTime = deliveryTime;
      Description = description;
   }

   public string Name { get; set; }
   public decimal Price { get; set; }
   public string DeliveryTime { get; set; }
   public string Description { get; set; }
}
