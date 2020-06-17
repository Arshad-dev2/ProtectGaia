using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProtectGaia.ActionFilters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProtectGaia.Controllers
{
    
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            //var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.Title = "404 Page not Found";
                    ViewBag.ErrorMessage = "Oops, The Page you are looking for can't be found!";
                    break;
                case 500:
                    ViewBag.Title = "500 Internal Server Error";
                    ViewBag.ErrorMessage = "The requested url is temporarily unavailable";
                    break;
            }
            return View("NotFound");
        }

    }
}
