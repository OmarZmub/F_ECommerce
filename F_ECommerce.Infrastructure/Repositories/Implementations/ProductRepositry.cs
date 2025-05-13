using AutoMapper;
using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Core.ViewModels.ProductVMs;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using F_ECommerce.Infrastructure.Repositories.Implementations.Generic;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace F_ECommerce.Infrastructure.Repositories.Implementations;

public class ProductRepositry : GenericRepositry<Product>, IProductRepositry
{
   private readonly AppDbContext context;
   private readonly IMapper mapper;
   private readonly IImageManagementService imageManagementService;
   public ProductRepositry(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
   {
      this.context = context;
      this.mapper = mapper;
      this.imageManagementService = imageManagementService;
   }
   public async Task<IEnumerable<ProductVM>> GetAllAsync(ProductParams productParams)
   {
      var query = context.Products
          .Include(m => m.Category)
          .Include(m => m.Photos)
          .AsNoTracking();



      //filtering by word
      if (!string.IsNullOrEmpty(productParams.Search))
      {
         var searchWords = productParams.Search.Split(' ');
         query = query.Where(m => searchWords.All(word =>

         m.Name.ToLower().Contains(word.ToLower()) ||
         m.Description.ToLower().Contains(word.ToLower())

         ));
      }



      //filtering by category Id
      if (productParams.CategoryId.HasValue)
         query = query.Where(m => m.CategoryId == productParams.CategoryId);

      if (!string.IsNullOrEmpty(productParams.Sort))
      {
         query = productParams.Sort switch
         {
            "PriceAce" => query.OrderBy(m => m.NewPrice),
            "PriceDce" => query.OrderByDescending(m => m.NewPrice),
            _ => query.OrderBy(m => m.Name),
         };
      }

      productParams.TotatlCount = query.Count();

      query = query.Skip((productParams.PageSize) * (productParams.PageNumber - 1)).Take(productParams.PageSize);


      var result = mapper.Map<List<ProductVM>>(query);

      return result;

   }
   public async Task<bool> AddAsync(AddProductVM productVM)
   {
      if (productVM == null)
         return false;

      var product = mapper.Map<Product>(productVM);

      await context.Products.AddAsync(product);
      await context.SaveChangesAsync();

      var ImagePath = await imageManagementService.AddImageAsync(productVM.Photo, productVM.Name);

      var photo = ImagePath.Select(path => new Photo
      {
         ImageName = path,
         ProductId = product.Id,
      }).ToList();
      await context.Photos.AddRangeAsync(photo);
      await context.SaveChangesAsync();
      return true;
   }


   public async Task<bool> UpdateAsync(UpdateProductVM updateProductVM)
   {
      if (updateProductVM is null)
      {
         return false;
      }
      var FindProduct = await context.Products.Include(m => m.Category)
          .Include(m => m.Photos)
          .FirstOrDefaultAsync(m => m.Id == updateProductVM.Id);

      if (FindProduct is null)
      {
         return false;
      }
      mapper.Map(updateProductVM, FindProduct);

      var FindPhoto = await context.Photos.Where(m => m.ProductId == updateProductVM.Id).ToListAsync();

      foreach (var item in FindPhoto)
      {
         imageManagementService.DeleteImageAsync(item.ImageName);
      }
      context.Photos.RemoveRange(FindPhoto);

      var ImagePath = await imageManagementService.AddImageAsync(updateProductVM.Photo, updateProductVM.Name);

      var photo = ImagePath.Select(path => new Photo
      {
         ImageName = path,
         ProductId = updateProductVM.Id,
      }).ToList();

      await context.Photos.AddRangeAsync(photo);

      await context.SaveChangesAsync();
      return true;

   }

   public async Task DeleteAsync(Product product)
   {
      var photo = await context.Photos.Where(m => m.ProductId == product.Id)
      .ToListAsync();
      foreach (var item in photo)
      {
         imageManagementService.DeleteImageAsync(item.ImageName);
      }
      context.Products.Remove(product);
      await context.SaveChangesAsync();
   }



}