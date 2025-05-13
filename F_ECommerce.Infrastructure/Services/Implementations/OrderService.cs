using AutoMapper;
using F_ECommerce.Core.Models.OrderModels;
using F_ECommerce.Core.ViewModels.OrderVMs;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace F_ECommerce.Infrastructure.Services.Implementations;

public class OrderService : IOrderService
{
   private readonly IUnitOfWork _unitOfWork;
   private readonly AppDbContext _context;
   private readonly IMapper _mapper;
   private readonly IPaymentService _paymentService;

   public OrderService(IUnitOfWork unitOfWork, AppDbContext context,
                      IMapper mapper, IPaymentService paymentService)
   {
      _unitOfWork = unitOfWork;
      _context = context;
      _mapper = mapper;
      _paymentService = paymentService;
   }

   public async Task<Order> CreateOrdersAsync(OrderVM orderVM, string BuyerEmail)
   {
      var basket = await _unitOfWork.CustomerBasket.GetByIdAsync(orderVM.BasketId);

      List<OrderItem> orderItems = new();

      foreach (var item in basket.basketItems)
      {
         var Product = await _unitOfWork.ProductRepositry.GetByIdAsync(item.Id);

         var orderItem = new OrderItem(Product.Id, item.Image, Product.Name,
                                       item.Price, item.Qunatity);

         orderItems.Add(orderItem);
      }

      var deliverMethod = await _context.DeliveryMethods
               .FirstOrDefaultAsync(m => m.Id == orderVM.DeliveryMethodId);

      var subTotal = orderItems.Sum(m => m.Price * m.Quntity);

      var ship = _mapper.Map<ShippingAddress>(orderVM.ShipAddress);

      var ExisitOrder = await _context.Orders
                                      .Where(m => m.PaymentIntentId == basket.PaymentIntentId)
                                      .FirstOrDefaultAsync();

      if (ExisitOrder is not null)
      {
         _context.Orders.Remove(ExisitOrder);

         await _paymentService.CreateOrUpdatePaymentAsync(basket.Id, deliverMethod.Id);
      }

      var order = new Order(BuyerEmail, subTotal, ship,
                            deliverMethod, orderItems, basket.PaymentIntentId);

      await _context.Orders.AddAsync(order);
      await _context.SaveChangesAsync();

      await _unitOfWork.CustomerBasket.DeleteAsync(orderVM.BasketId);

      return order;
   }


   public async Task<IReadOnlyList<OrderToReturnVM>> GetAllOrdersForUserAsync(string BuyerEmail)
   {
      var Order = await _context.Orders.Where(m => m.BuyerEmail == BuyerEmail)
                                       .Include(inc => inc.OrderItems)
                                       .Include(inc => inc.DeliveryMethod)
                                       .ToListAsync();

      var result = _mapper.Map<IReadOnlyList<OrderToReturnVM>>(Order);

      result = result.OrderByDescending(m => m.Id).ToList();

      return result;
   }

   public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
   => await _context.DeliveryMethods.AsNoTracking().ToListAsync();

   public async Task<OrderToReturnVM> GetOrderByIdAsync(long Id, string BuyerEmail)
   {
      var order = await _context.Orders.Where(m => m.Id == Id && m.BuyerEmail == BuyerEmail)
                                       .Include(inc => inc.OrderItems)
                                       .Include(inc => inc.DeliveryMethod)
                                       .FirstOrDefaultAsync();

      var result = _mapper.Map<OrderToReturnVM>(order);

      return result;
   }

}
