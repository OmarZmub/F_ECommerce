using F_ECommerce.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.Models.ProductModels;

public class Category : BaseEntity
{
   public string Name { get; set; }
   public string Description { get; set; }
}
