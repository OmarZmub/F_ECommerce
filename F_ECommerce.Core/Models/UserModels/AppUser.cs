using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.Models.UserModels;

public class AppUser : IdentityUser
{
   // Id (Guid)
   // Username
   // Email
   // PhoneNumber
   public string DispalyName { get; set; }
   public Address Address { get; set; }

}

