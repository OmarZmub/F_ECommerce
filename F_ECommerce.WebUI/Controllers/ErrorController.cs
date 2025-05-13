using Microsoft.AspNetCore.Mvc;

namespace Ecom.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Index(int statusCode)
        {
            var model = new { StatusCode = statusCode, Message = GetErrorMessage(statusCode) };
            return View("Error", model);
        }

        private string GetErrorMessage(int statusCode)
        {
            return statusCode switch
            {
                404 => "Page not found.",
                500 => "Internal server error.",
                _ => "An unexpected error occurred."
            };
        }
    }
}