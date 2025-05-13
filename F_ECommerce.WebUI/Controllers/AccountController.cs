using AutoMapper;
using F_ECommerce.Infrastructure.Services.Abstractions;
using F_ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using F_ECommerce.Core.ViewModels.AccountVMs;
using F_ECommerce.Core.Models.UserModels;
using F_ECommerce.Core.ViewModels.OrderVMs;

namespace Ecom.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAddress()
        {
            var address = await _work.Auth.GetUserAddress(User.FindFirst(ClaimTypes.Email)?.Value);
            var result = _mapper.Map<ShipAddressVM>(address);
            return View(result);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(-1)
            });
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetUserName()
        {
            var model = new ResponseAPI(200, User.Identity.Name);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> IsUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Authenticated");
            }
            return View("NotAuthenticated");
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateAddress()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UpdateAddress(ShipAddressVM addressVM)
        {
            if (!ModelState.IsValid)
            {
                return View(addressVM);
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = _mapper.Map<Address>(addressVM);
            var result = await _work.Auth.UpdateAddress(email, address);

            if (result)
            {
                return RedirectToAction("GetAddress");
            }
            return View(addressVM);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            string result = await _work.Auth.RegisterAsync(registerVM);
            if (result != "done")
            {
                ModelState.AddModelError("", result);
                return View(registerVM);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            string result = await _work.Auth.LoginAsync(loginVM);
            if (result.StartsWith("please"))
            {
                ModelState.AddModelError("", result);
                return View(loginVM);
            }

            Response.Cookies.Append("token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1)
            });
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ActivateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ActivateAccount(ActiveAccountVM accountVM)
        {
            if (!ModelState.IsValid)
            {
                return View(accountVM);
            }

            var result = await _work.Auth.ActiveAccount(accountVM);
            if (result)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Account activation failed");
            return View(accountVM);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required");
                return View();
            }

            var result = await _work.Auth.SendEmailForForgetPassword(email);
            if (result)
            {
                ViewBag.Message = "Password reset email sent";
                return View("ForgotPasswordConfirmation");
            }
            ModelState.AddModelError("", "Failed to send reset email");
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(RestPasswordVM restPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(restPasswordVM);
            }

            var result = await _work.Auth.ResetPassword(restPasswordVM);
            if (result == "done")
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Password reset failed");
            return View(restPasswordVM);
        }
    }
}