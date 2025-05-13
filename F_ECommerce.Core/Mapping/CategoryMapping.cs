using AutoMapper;
using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Core.ViewModels.CategoryVMs;

namespace F_ECommerce.Core.Mapping;

public class CategoryMapping : Profile
{
   public CategoryMapping()
   {
      CreateMap<CategoryVM, Category>().ReverseMap();
      CreateMap<UpdateCategoryVM, Category>().ReverseMap();
   }
}
