using F_ECommerce.Core.ViewModels.RatingVMs;
using F_ECommerce.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecom.Web.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRating _rating;

        public RatingsController(IRating rating)
        {
            _rating = rating;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int productId)
        {
            var ratings = await _rating.GetAllRatingForProductAsync(productId);
            ViewBag.ProductId = productId;
            return View(ratings);
        }

        [HttpGet]
        public ActionResult Create(int productId)
        {
            var model = new RatingVM { ProductId = productId };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(RatingVM ratings)
        {
            if (!ModelState.IsValid)
            {
                return View(ratings);
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _rating.AddRatingAsync(ratings, email);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to add rating.");
                return View(ratings);
            }
            return RedirectToAction("Index", new { productId = ratings.ProductId });
        }
    }
}