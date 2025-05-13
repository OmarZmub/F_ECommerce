using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using F_ECommerce.Infrastructure.Repositories.Implementations.Generic;
using F_ECommerce.Infrastructure.Repositories.Implementations;
using F_ECommerce.Infrastructure.Services.Abstractions;
using F_ECommerce.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using F_ECommerce.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace F_ECommerce.Infrastructure.DependencyInjection;

public static class InfrastructureRegisteration
{

   public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services,
                                                             IConfiguration configuration)
   {
      //apply DbContext
      services.AddDbContext<AppDbContext>(op =>
      {
         op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
      });

      services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));

      //apply Unit OF Work
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      //register email sender
      services.AddScoped<IEmailService, EmailService>();

      //register IOrder Service
      services.AddScoped<IOrderService, OrderService>();

      services.AddScoped<IRating, RatingRepositry>();
      //register token
      services.AddScoped<IGenerateToken, GenerateToken>();

      //register payment service
      services.AddScoped<IPaymentService, PaymentService>();


      ////apply Redis Connectoon
      //services.AddSingleton<IConnectionMultiplexer>(i =>
      //{
      //   var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
      //   return ConnectionMultiplexer.Connect(config);
      //});

      services.AddSingleton<IImageManagementService, ImageManagementService>();
      services.AddSingleton<IFileProvider>(
          new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


      services.AddIdentity<AppUser, IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();


      services.AddAuthentication(x =>
      {
         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      })
      .AddCookie(x =>
      {
         x.Cookie.Name = "token";
         x.Events.OnRedirectToLogin = context =>
         {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
         };
      })
      .AddJwtBearer(x =>
      {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
            ValidateIssuer = true,
            ValidIssuer = configuration["Token:Issuer"],
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
         };
         x.Events = new JwtBearerEvents
         {

            OnMessageReceived = context =>
            {
               var token = context.Request.Cookies["token"];
               context.Token = token;
               return Task.CompletedTask;
            }
         };
      });


      return services;
   }
}