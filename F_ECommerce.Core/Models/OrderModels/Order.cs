using F_ECommerce.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.Models.OrderModels;

public class Order : BaseEntity
{
   public Order() { }
   public Order(string buyerEmail, decimal subTotal,
                ShippingAddress shippingAddress,
                DeliveryMethod deliveryMethod,
                IReadOnlyList<OrderItem> orderItems, string PaymentIntentId)
   {
      BuyerEmail = buyerEmail;
      SubTotal = subTotal;
      this.ShippingAddress = shippingAddress;
      this.DeliveryMethod = deliveryMethod;
      this.OrderItems = orderItems;
      this.PaymentIntentId = PaymentIntentId;
   }

   public string BuyerEmail { get; set; }
   public decimal SubTotal { get; set; }
   public DateTime OrderDate { get; set; } = DateTime.Now;
   public ShippingAddress ShippingAddress { get; set; }
   public string PaymentIntentId { get; set; }
   public IReadOnlyList<OrderItem> OrderItems { get; set; }
   public DeliveryMethod DeliveryMethod { get; set; }


   public Status Status { get; set; } = Status.Pending;

   public decimal GetTotal()
   {
      return SubTotal + DeliveryMethod.Price;
   }

}
