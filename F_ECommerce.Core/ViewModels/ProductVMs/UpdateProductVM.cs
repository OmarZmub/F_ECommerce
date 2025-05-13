namespace F_ECommerce.Core.ViewModels.ProductVMs;

public record UpdateProductVM : AddProductVM
{
   public int Id { get; set; }
}