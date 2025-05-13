using F_ECommerce.Core.Models.BasketModels;
using F_ECommerce.Core.Models.OrderModels;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace F_ECommerce.Infrastructure.Services.Implementations;

public class PaymentService : IPaymentService
{
   private readonly IUnitOfWork work;
   private readonly IConfiguration configuration;
   private readonly AppDbContext _context;
   public PaymentService(IUnitOfWork work, IConfiguration configuration, AppDbContext context)
   {
      this.work = work;
      this.configuration = configuration;
      _context = context;
   }
   public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(long basketId, long? delivertMethodId)
   {
      CustomerBasket basket = await work.CustomerBasket.GetByIdAsync(basketId);
      StripeConfiguration.ApiKey = configuration["StripSetting:secretKey"];
      decimal shippingPrice = 0m;
      if (delivertMethodId.HasValue)
      {
         var delivery = await _context.DeliveryMethods.AsNoTracking()
             .FirstOrDefaultAsync(m => m.Id == delivertMethodId.Value);
         shippingPrice = delivery.Price;
      }
      foreach (var item in basket.basketItems)
      {
         var product = await work.ProductRepositry.GetByIdAsync(item.Id);
         item.Price = product.NewPrice;
      }
      PaymentIntentService paymentIntentService = new();
      PaymentIntent _intent;
      if (string.IsNullOrEmpty(basket.PaymentIntentId))
      {
         var option = new PaymentIntentCreateOptions
         {
            Amount = (long) basket.basketItems.Sum(m => m.Qunatity * (m.Price * 100)) + (long) (shippingPrice * 100),

            Currency = "USD",
            PaymentMethodTypes = new List<string> { "card" }
         };
         _intent = await paymentIntentService.CreateAsync(option);
         basket.PaymentIntentId = _intent.Id;
         basket.ClientSecret = _intent.ClientSecret;
      }
      else
      {
         var option = new PaymentIntentUpdateOptions
         {
            Amount = (long) basket.basketItems.Sum(m => m.Qunatity * (m.Price * 100)) + (long) (shippingPrice * 100),

         };
         await paymentIntentService.UpdateAsync(basket.PaymentIntentId, option);
      }
      await work.CustomerBasket.UpdateAsync(basket);
      return basket;
   }

   public async Task<Order> UpdateOrderFaild(string PaymentInten)
   {
      var order = await _context.Orders.FirstOrDefaultAsync(m => m.PaymentIntentId == PaymentInten);
      if (order is null)
      {
         return null;
      }
      order.Status = Status.PaymentFaild;
      _context.Orders.Update(order);
      await _context.SaveChangesAsync();
      return order;
   }

   public async Task<Order> UpdateOrderSuccess(string PaymentInten)
   {
      var order = await _context.Orders.FirstOrDefaultAsync(m => m.PaymentIntentId == PaymentInten);
      if (order is null)
      {
         return null;
      }
      order.Status = Status.PaymentReceived;
      _context.Orders.Update(order);
      await _context.SaveChangesAsync();
      return order;
   }
}