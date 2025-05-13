namespace F_ECommerce.Core.Models.BasketModels;

public class BasketItem
{
   public long Id { get; set; }
   public string Name { get; set; }
   public string Description { get; set; }
   public string Image { get; set; }
   public int Qunatity { get; set; }
   public decimal Price { get; set; }
   public string Category { get; set; }

}
