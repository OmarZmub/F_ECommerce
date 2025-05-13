using AutoMapper;
using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Data.Context;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using F_ECommerce.Infrastructure.Repositories.Implementations;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace F_ECommerce.Infrastructure.Services.Implementations;

public class UnitOfWork : IUnitOfWork
{
   private readonly AppDbContext _context;
   private readonly IMapper _mapper;
   private readonly IImageManagementService _imageManagementService;
   private readonly UserManager<AppUser> userManager;
   private readonly IEmailService emailService;
   private readonly SignInManager<AppUser> signInManager;
   private readonly IGenerateToken token;
   public ICategoryRepositry CategoryRepositry { get; }

   public IPhotoRepositry PhotoRepositry { get; }

   public IProductRepositry ProductRepositry { get; }

   public ICustomerBasketRepositry CustomerBasket { get; }

   public IAuthentication Auth { get; }

   public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService,

        UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken token)
   {
      _context = context;
      _mapper = mapper;
      _imageManagementService = imageManagementService;
      this.userManager = userManager;
      this.emailService = emailService;
      this.signInManager = signInManager;
      this.token = token;
      CategoryRepositry = new CategoryRepositry(_context);
      PhotoRepositry = new PhotoRepositry(_context);
      ProductRepositry = new ProductRepositry(_context, _mapper, _imageManagementService);
      CustomerBasket = new CustomerBasketRepository(_context);
      Auth = new AuthenticationRepositry(userManager, emailService, signInManager, token, context);
   }
}