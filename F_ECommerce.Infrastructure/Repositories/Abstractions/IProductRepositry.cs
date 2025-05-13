using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Core.ViewModels.ProductVMs;

namespace F_ECommerce.Infrastructure.Repositories.Abstractions;

public interface IProductRepositry : IGenericRepositry<Product>
{
   // for futuer
   Task<IEnumerable<ProductVM>> GetAllAsync(ProductParams productParams);
   Task<bool> AddAsync(AddProductVM productVM);
   Task<bool> UpdateAsync(UpdateProductVM updateProductVM);
   Task DeleteAsync(Product product);
}

