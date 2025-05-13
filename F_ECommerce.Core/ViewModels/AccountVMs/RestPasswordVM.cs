namespace F_ECommerce.Core.ViewModels.AccountVMs;

public record RestPasswordVM : LoginVM
{
   public string Token { get; set; }
}
