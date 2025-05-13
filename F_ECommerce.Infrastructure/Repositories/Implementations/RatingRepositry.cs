using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Core.ViewModels.RatingVMs;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F_ECommerce.Infrastructure.Repositories.Implementations;

public class RatingRepositry : IRating
{
   private readonly AppDbContext _context;
   private readonly UserManager<AppUser> _userManager;
   public RatingRepositry(AppDbContext context, UserManager<AppUser> userManager)
   {
      _context = context;
      _userManager = userManager;
   }

   public async Task<bool> AddRatingAsync(RatingVM ratingVM, string email)
   {
      var finduser = await _userManager.FindByEmailAsync(email);

      if (await _context.Ratings.AsNoTracking()
                        .AnyAsync(m => m.AppUserId == finduser.Id && m.ProductId == ratingVM.ProductId))
      {
         return false;
      }

      var rating = new Rating()
      {
         AppUserId = finduser.Id,
         ProductId = ratingVM.ProductId,
         Stars = ratingVM.Stars,
         content = ratingVM.Content,

      };
      await _context.Ratings.AddAsync(rating);
      await _context.SaveChangesAsync();

      var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == ratingVM.ProductId);

      var ratings = await _context.Ratings.AsNoTracking().Where(m => m.ProductId == product.Id).ToListAsync();

      if (ratings.Count > 0)
      {
         double average = ratings.Average(m => m.Stars);
         double roundedReview = Math.Round(average * 2, mode: MidpointRounding.AwayFromZero) / 2;
         product.rating = roundedReview;
      }
      else
      {
         product.rating = ratingVM.Stars;
      }
      await _context.SaveChangesAsync();
      return true;

   }

   public async Task<IReadOnlyList<ReturnRatingVM>> GetAllRatingForProductAsync(int productId)
   {
      var ratings = await _context.Ratings.Include(m => m.AppUser)
           .AsNoTracking().Where(m => m.ProductId == productId).ToListAsync();

      return ratings.Select(m => new ReturnRatingVM
      {
         Content = m.content,
         ReviewTime = m.Review,
         Stars = m.Stars,
         UserName = m.AppUser.UserName,
      }).ToList();
   }
}
