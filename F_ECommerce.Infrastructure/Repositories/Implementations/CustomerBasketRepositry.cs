using F_ECommerce.Core.Models.BasketModels;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using F_ECommerce.Infrastructure.Repositories.Implementations.Generic;

namespace F_ECommerce.Infrastructure.Repositories.Implementations;

public class CustomerBasketRepository : GenericRepositry<CustomerBasket>, ICustomerBasketRepositry
{
   public CustomerBasketRepository(AppDbContext context) : base(context)
   {
   }
}
