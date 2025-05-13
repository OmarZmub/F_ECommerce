using AutoMapper;
using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Core.ViewModels.ProductVMs;

namespace F_ECommerce.Core.Mapping;

public class ProductMapping : Profile
{
   public ProductMapping()
   {
      CreateMap<Product, ProductVM>
          ().ForMember(x => x.CategoryName,
          op => op.MapFrom(src => src.Category.Name))
          .ReverseMap();
      ;
      CreateMap<Photo, PhotoVM>().ReverseMap();
      CreateMap<AddProductVM, Product>()
      .ForMember(m => m.Photos, op => op.Ignore())
      .ReverseMap();

      CreateMap<UpdateProductVM, Product>()
    .ForMember(m => m.Photos, op => op.Ignore())
    .ReverseMap();

   }
}
