using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.ViewModels.RatingVMs;

public class RatingVM
{
   public int Stars { get; set; }
   public string Content { get; set; }

   public int ProductId { get; set; }
}
