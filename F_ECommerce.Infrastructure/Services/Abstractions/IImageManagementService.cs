using Microsoft.AspNetCore.Http;

namespace F_ECommerce.Infrastructure.Services.Abstractions;

public interface IImageManagementService
{
   Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
   void DeleteImageAsync(string src);
}

