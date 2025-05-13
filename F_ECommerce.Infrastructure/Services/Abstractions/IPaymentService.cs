using F_ECommerce.Core.Models.BasketModels;
using F_ECommerce.Core.Models.OrderModels;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IPaymentService
{
   Task<CustomerBasket> CreateOrUpdatePaymentAsync(long basketId, long? deliveryId);
   Task<Order> UpdateOrderSuccess(string PaymentInten);
   Task<Order> UpdateOrderFaild(string PaymentInten);
}

