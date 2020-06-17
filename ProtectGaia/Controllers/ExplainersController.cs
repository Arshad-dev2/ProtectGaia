using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtectGaia.ActionFilters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProtectGaia.Controllers
{
    [BasicAuthenticationAttribute("ecoMorph", "Password123", BasicRealm = "ecoMorph")]
    public class ExplainersController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TransportEmissions()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EnergyConsumption()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HouseholdPractices()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HealthImpacts()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ClimateChange()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MakeAChange()
        {
            return View();
        }



    }
}
