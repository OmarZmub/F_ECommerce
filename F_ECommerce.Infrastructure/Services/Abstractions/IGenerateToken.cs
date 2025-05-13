using F_ECommerce.Core.Models.UserModels;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IGenerateToken
{
   string GetAndCreateToken(AppUser user);
}

