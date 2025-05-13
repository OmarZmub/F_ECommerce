namespace F_ECommerce.Core.ViewModels.AccountVMs;

public record RegisterVM : LoginVM
{
   public string UserName { get; set; }
   public string DisplayName { get; set; }

}
