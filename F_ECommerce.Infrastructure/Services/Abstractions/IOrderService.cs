using F_ECommerce.Core.Models.OrderModels;
using F_ECommerce.Core.ViewModels.OrderVMs;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IOrderService
{
   Task<Order> CreateOrdersAsync(OrderVM orderVM, string BuyerEmail);
   Task<IReadOnlyList<OrderToReturnVM>> GetAllOrdersForUserAsync(string BuyerEmail);
   Task<OrderToReturnVM> GetOrderByIdAsync(long Id, string BuyerEmail);
   Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
}

