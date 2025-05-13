namespace F_ECommerce.Core.ViewModels.OrderVMs;

public record ShipAddressVM
{
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string City { get; set; }
   public string ZipCode { get; set; }
   public string Street { get; set; }
   public string State { get; set; }
}