using F_ECommerce.Core.ViewModels.EmailVMs;
using F_ECommerce.Core.ViewModels.OrderVMs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IEmailService
{
   public Task SendEmail(EmailVM email);

}
