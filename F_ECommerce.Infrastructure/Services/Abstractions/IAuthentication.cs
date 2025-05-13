using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Core.ViewModels.AccountVMs;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IAuthentication
{
   Task<string> RegisterAsync(RegisterVM registerVM);
   Task<string> LoginAsync(LoginVM login);
   Task<bool> SendEmailForForgetPassword(string email);
   Task<string> ResetPassword(RestPasswordVM restPassword);
   Task<bool> ActiveAccount(ActiveAccountVM accountVM);
   Task<bool> UpdateAddress(string email, Address address);
   Task<Address> GetUserAddress(string email);
}
