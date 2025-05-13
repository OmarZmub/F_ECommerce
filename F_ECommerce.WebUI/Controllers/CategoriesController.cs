using AutoMapper;
using F_ECommerce.Core.Models.ProductModels;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using F_ECommerce.Core.ViewModels.CategoryVMs;

namespace Ecom.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var categories = await _work.CategoryRepositry.GetAllAsync();
                return View(categories);
            }
            catch
            {
                ViewBag.Error = "An error occurred while fetching categories.";
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var category = await _work.CategoryRepositry.GetByIdAsync(id);
                if (category == null)
                {
                    ViewBag.Error = $"Category with ID {id} not found.";
                    return View("Error");
                }
                return View(category);
            }
            catch
            {
                ViewBag.Error = "An error occurred while fetching the category.";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            try
            {
                var category = _mapper.Map<Category>(categoryVM);
                await _work.CategoryRepositry.AddAsync(category);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while adding the category.");
                return View(categoryVM);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _work.CategoryRepositry.GetByIdAsync(id);
            if (category == null)
            {
                ViewBag.Error = $"Category with ID {id} not found.";
                return View("Error");
            }
            var categoryVM = _mapper.Map<UpdateCategoryVM>(category);
            return View(categoryVM);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateCategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            try
            {
                var category = _mapper.Map<Category>(categoryVM);
                await _work.CategoryRepositry.UpdateAsync(category);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while updating the category.");
                return View(categoryVM);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _work.CategoryRepositry.GetByIdAsync(id);
            if (category == null)
            {
                ViewBag.Error = $"Category with ID {id} not found.";
                return View("Error");
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _work.CategoryRepositry.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Error = "An error occurred while deleting the category.";
                return View("Error");
            }
        }
    }
}