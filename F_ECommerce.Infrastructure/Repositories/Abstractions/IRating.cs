using F_ECommerce.Core.ViewModels.RatingVMs;

namespace F_ECommerce.Infrastructure.Repositories.Abstractions;

public interface IRating
{
   Task<bool> AddRatingAsync(RatingVM ratingVM, string email);
   Task<IReadOnlyList<ReturnRatingVM>> GetAllRatingForProductAsync(int productId);
}

