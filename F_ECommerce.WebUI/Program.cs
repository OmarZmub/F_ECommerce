using F_ECommerce.Infrastructure.DependencyInjection;
namespace F_ECommerce.WebUI;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllersWithViews();

      ////////////////////////////////////

      builder.Services.InfrastructureConfiguration(builder.Configuration);
      builder.Services.AddMemoryCache();

      builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      //////////////////////////////////////

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
         app.UseExceptionHandler("/Home/Error");
         // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
         app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      app.Run();
   }
}
