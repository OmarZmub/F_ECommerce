using AutoMapper;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Web.Controllers
{
    public class BugController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public BugController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> NotFound()
        {
            var category = await _work.CategoryRepositry.GetByIdAsync(100);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<ActionResult> ServerError()
        {
            try
            {
                var category = await _work.CategoryRepositry.GetByIdAsync(100);
                category.Name = ""; // Simulate error
                return View(category);
            }
            catch
            {
                return View("ServerError");
            }
        }

        [HttpGet]
        public ActionResult BadRequest(int? id)
        {
            if (id.HasValue)
            {
                return View("BadRequest");
            }
            return View("BadRequestNoId");
        }
    }
}