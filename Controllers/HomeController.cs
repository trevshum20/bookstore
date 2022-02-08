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
                return RedirectToAction("Quadrant");

            }
            else
            {
                return View("AddEdit", req);
            }

        }


        [HttpGet]
        public IActionResult Quadrant()
        {
            var tasks = YareContext.Responses
                .Include(x => x.Category)
                .Where(x => x.Completed == "false")
                .ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int taskId)
        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            return View("AddEdit", task);

        }

        [HttpPost]
        public IActionResult Edit(bretheren bruv)
        {
            if (ModelState.IsValid)
            {
                YareContext.Update(bruv);
                YareContext.SaveChanges();
                return RedirectToAction("Quadrant");
            }
            else
            {
                return View("AddEdit", bruv);
            }
        }

        [HttpGet]
        public IActionResult Complete(int taskId)
        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            task.Completed = "true";
            YareContext.Update(task);
            YareContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult Delete(int taskId)
        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete (bretheren ar)
        {
            YareContext.Responses.Remove(ar);
            YareContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult AllTasks()
        {
            var tasks = YareContext.Responses
                .Include(x => x.Category)
                .ToList();
            return View(tasks);
        }

    }
}
