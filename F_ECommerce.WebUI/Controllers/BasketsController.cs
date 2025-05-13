using AutoMapper;
using F_ECommerce.Core.Models.BasketModels;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.Web.Controllers
{
    public class BasketsController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public BasketsController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(long id)
        {
            var basket = await _work.CustomerBasket.GetByIdAsync(id);
            if (basket == null)
            {
                basket = new CustomerBasket();
            }
            return View(basket);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update(CustomerBasket basket)
        {
            if (!ModelState.IsValid)
            {
                return View(basket);
            }

            await _work.CustomerBasket.UpdateAsync(basket);
           
            return RedirectToAction("Index", new { id = basket.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            var basket = await _work.CustomerBasket.GetByIdAsync(id);
            if (basket == null)
            {
                ViewBag.Error = $"Basket with ID {id} not found.";
                return View("Error");
            }
            return View(basket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await _work.CustomerBasket.DeleteAsync(id);
            
            return RedirectToAction("Index");
        }
    }
}