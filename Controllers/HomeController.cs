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
            if (ModelState.IsValid)  // add the new task if the form is completed correctly
            {
                YareContext.Add(req);
                YareContext.SaveChanges();
                return RedirectToAction("Quadrant");

            }
            else
            {
                return View("AddEdit", req); // if form filled out wrong, return them to the add edit page with the information they entered still populated
            }

        }


        [HttpGet]
        public IActionResult Quadrant()
        {
            var tasks = YareContext.Responses // view incomplete tasks
                .Include(x => x.Category)
                .Where(x => x.Completed == "false")
                .ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int taskId) // shows the edit view
        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            return View("AddEdit", task);

        }

        [HttpPost]
        public IActionResult Edit(bretheren bruv)
        {
            if (ModelState.IsValid) // if the form is correct, save their edits
            {
                YareContext.Update(bruv);
                YareContext.SaveChanges();
                return RedirectToAction("Quadrant");
            }
            else
            {
                return View("AddEdit", bruv); // if form filled out wrong, return them to the add edit page with the information they entered still populated
            }
        }

        [HttpGet]
        public IActionResult Complete(int taskId) // when the user presses the "complete" button on the quadrants page, that task's "completed" attribute is
                                                 // marked changed to "true"
        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            task.Completed = "true";
            YareContext.Update(task);
            YareContext.SaveChanges();
            return RedirectToAction("Quadrant"); // when it redirects to the quadrant action, that task disappears because it is completed
        }

        public IActionResult Incomplete(int taskId) // There is a view that shows all tasks regardless of their completion status
                                                    // when the user presses the "incomplete" button on that page, that task's "completed" attribute is
                                                    // marked changed to "false"

        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            task.Completed = "false";
            YareContext.Update(task);
            YareContext.SaveChanges();
            return RedirectToAction("AllTasks"); // if the user goes back to the quadrants view, that task will appear and marked as incomplete
        }

        [HttpGet]
        public IActionResult Delete(int taskId)
        {
            var task = YareContext.Responses.Single(x => x.TaskID == taskId);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete (bretheren ar) // delete a record
        {
            YareContext.Responses.Remove(ar);
            YareContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult AllTasks() // similar to quadrant view but shows all task regardless of their completion status
                                        
        {
            var tasks = YareContext.Responses
                .Include(x => x.Category)
                .ToList();
            return View(tasks); 
        }

    }
}
