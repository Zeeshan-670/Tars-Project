using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semester_End.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Mails()
        {
            return View();
        }

        public IActionResult Branch()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }

        public IActionResult Tracking()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }


        public IActionResult Shipment()
        {
            return View();
        }



    }
}
