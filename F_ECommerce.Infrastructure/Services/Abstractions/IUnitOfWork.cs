using F_ECommerce.Infrastructure.Repositories.Abstractions;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IUnitOfWork
{
   public ICategoryRepositry CategoryRepositry { get; }
   public IPhotoRepositry PhotoRepositry { get; }
   public IProductRepositry ProductRepositry { get; }
   public ICustomerBasketRepositry CustomerBasket { get; }
   public IAuthentication Auth { get; }
}
