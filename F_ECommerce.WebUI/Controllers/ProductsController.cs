using AutoMapper;
using F_ECommerce.Core.ViewModels.ProductVMs;
using F_ECommerce.Infrastructure.Services.Abstractions;
using F_ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _service;

        public ProductsController(IUnitOfWork work, IMapper mapper, IImageManagementService service)
        {
            _work = work;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index([FromQuery] ProductParams productParams)
        {
            try
            {
                var products = await _work.ProductRepositry.GetAllAsync(productParams);
                var model = new Pagination<ProductVM>(productParams.PageNumber, productParams.PageSize, productParams.TotatlCount, products);
                return View(model);
            }
            catch
            {
                ViewBag.Error = "An error occurred while fetching products.";
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var product = await _work.ProductRepositry.GetByIdAsync(id, x => x.Category, x => x.Photos);
                if (product == null)
                {
                    ViewBag.Error = $"Product with ID {id} not found.";
                    return View("Error");
                }
                var result = _mapper.Map<ProductVM>(product);
                return View(result);
            }
            catch
            {
                ViewBag.Error = "An error occurred while fetching the product.";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            try
            {
                await _work.ProductRepositry.AddAsync(productVM);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while adding the product.");
                return View(productVM);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _work.ProductRepositry.GetByIdAsync(id, x => x.Category, x => x.Photos);
            if (product == null)
            {
                ViewBag.Error = $"Product with ID {id} not found.";
                return View("Error");
            }
            var productVM = _mapper.Map<UpdateProductVM>(product);
            return View(productVM);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateProductVM updateProductVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateProductVM);
            }

            try
            {
                await _work.ProductRepositry.UpdateAsync(updateProductVM);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while updating the product.");
                return View(updateProductVM);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _work.ProductRepositry.GetByIdAsync(id, x => x.Photos, x => x.Category);
            if (product == null)
            {
                ViewBag.Error = $"Product with ID {id} not found.";
                return View("Error");
            }
            var productVM = _mapper.Map<ProductVM>(product);
            return View(productVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _work.ProductRepositry.GetByIdAsync(id, x => x.Photos, x => x.Category);
                await _work.ProductRepositry.DeleteAsync(product);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Error = "An error occurred while deleting the product.";
                return View("Error");
            }
        }
    }
}