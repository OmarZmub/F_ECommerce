using F_ECommerce.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.Models.BasketModels;

public class CustomerBasket : BaseEntity
{
   public CustomerBasket()
   {

   }
   public CustomerBasket(long id)
   {
      Id = id;
   }

   public string PaymentIntentId { get; set; }
   public string ClientSecret { get; set; }

   public List<BasketItem> basketItems { get; set; } = new List<BasketItem>(); //value

}

