using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtectGaia.Interface;
using ProtectGaia.Models;

namespace ProtectGaia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMeetupApi _iMeetupApi;

        public HomeController(ILogger<HomeController> logger, IMeetupApi iMeetupApi)
        {
            _logger = logger;
            _iMeetupApi = iMeetupApi;

        }

        public IActionResult Index()
        {
            //var result = _iMeetupApi.UpcomingEvents();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
