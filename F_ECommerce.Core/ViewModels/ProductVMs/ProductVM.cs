using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.ViewModels.ProductVMs;

public record ProductVM
{
   public int Id { get; set; }
   public string Name { get; set; }
   public string Description { get; set; }
   public decimal NewPrice { get; set; }
   public decimal OldPrice { get; set; }
   public virtual List<PhotoVM> Photos { get; set; }
   public string CategoryName { get; set; }
   public double rating { get; set; }

}
