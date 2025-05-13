namespace F_ECommerce.Core.ViewModels.RatingVMs;

public class ReturnRatingVM
{
   public int Stars { get; set; }
   public string Content { get; set; }

   public string UserName { get; set; }
   public DateTime ReviewTime { get; set; }
}