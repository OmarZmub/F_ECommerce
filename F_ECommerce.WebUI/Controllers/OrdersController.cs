using F_ECommerce.Core.ViewModels.OrderVMs;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderVM orderVM)
        {
            if (!ModelState.IsValid)
            {
                return View(orderVM);
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await _orderService.CreateOrdersAsync(orderVM, email);
            if (order == null)
            {
                ModelState.AddModelError("", "Failed to create order.");
                return View(orderVM);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var orders = await _orderService.GetAllOrdersForUserAsync(email);
            return View(orders);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order == null)
            {
                ViewBag.Error = $"Order with ID {id} not found.";
                return View("Error");
            }
            return View(order);
        }

        [HttpGet]
        public async Task<ActionResult> DeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodAsync();
            return View(deliveryMethods);
        }
    }
}