using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.ViewModels.OrderVMs;

public record OrderVM
{
   public long DeliveryMethodId { get; set; }

   public long BasketId { get; set; }
   public ShipAddressVM ShipAddress { get; set; }
}
