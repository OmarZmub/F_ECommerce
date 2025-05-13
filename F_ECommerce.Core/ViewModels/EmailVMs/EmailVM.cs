using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Core.ViewModels.EmailVMs;

public class EmailVM
{
   public EmailVM(string to, string from, string subject, string content)
   {
      To = to;
      From = from;
      Subject = subject;
      Content = content;
   }

   public string To { get; set; }
   public string From { get; set; }
   public string Subject { get; set; }
   public string Content { get; set; }
}
