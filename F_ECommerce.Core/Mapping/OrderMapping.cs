using AutoMapper;
using F_ECommerce.Core.Models.OrderModels;
using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Core.ViewModels.OrderVMs;

namespace F_ECommerce.Core.Mapping;

public class OrderMapping : Profile
{
   public OrderMapping()
   {
      CreateMap<Order, OrderToReturnVM>()
          .ForMember(d => d.deliveryMethod,
          o => o.
          MapFrom(s => s.DeliveryMethod.Name))
          .ReverseMap();

      CreateMap<OrderItem, OrderItemVM>().ReverseMap();
      CreateMap<ShippingAddress, ShipAddressVM>().ReverseMap();
      CreateMap<Address, ShipAddressVM>().ReverseMap();
   }
}
