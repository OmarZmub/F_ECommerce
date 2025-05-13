using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.ViewModels.AccountVMs;

public record LoginVM
{
   public string Email { get; set; }
   public string Password { get; set; }
}
