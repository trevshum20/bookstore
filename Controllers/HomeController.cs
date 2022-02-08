using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TaskContext YareContext { get; set; }

        public HomeController(ILogger<HomeController> logger, TaskContext jotaro)
        {
            _logger = logger;
            YareContext = jotaro;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEdit(bretheren req)
        {
            if (ModelState.IsValid)
            {
                YareContext.Add(req);
                YareContext.SaveChanges();
                return View("Confirmation");

            }
            else
            {
                return View("Quadrants");
            }
            
        }



        public IActionResult Quadrants()
        {
            return View();
        }
    }
}
