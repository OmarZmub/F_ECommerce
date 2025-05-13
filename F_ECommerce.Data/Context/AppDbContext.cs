using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Core.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using F_ECommerce.Core.Models.OrderModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using F_ECommerce.Core.Models.BasketModels;

namespace F_ECommerce.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
   {
   }
   public virtual DbSet<Category> Categories { get; set; }
   public virtual DbSet<Product> Products { get; set; }
   public virtual DbSet<Photo> Photos { get; set; }
   public virtual DbSet<Address> Addresses { get; set; }
   public virtual DbSet<Order> Orders { get; set; }
   public virtual DbSet<OrderItem> OrderItems { get; set; }
   public virtual DbSet<Rating> Ratings { get; set; }
   public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
   public virtual DbSet<CustomerBasket> CustomerBaskets { get; set; }


   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
   }
}
