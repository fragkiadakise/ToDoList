using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services.Interfaces;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMockAuthService _mockAuthService;
        private readonly IToDoListItemService _toDoListItemService;
        private string authenticatedUser;

        public HomeController(IMockAuthService mockAuthService, IToDoListItemService toDoListItemService, ILogger<HomeController> logger)
        {
            _mockAuthService = mockAuthService;
            _toDoListItemService = toDoListItemService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                authenticatedUser = HttpContext.Session.GetString("AuthenticateUser");

                if (authenticatedUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.ErrorMessage = "Wrong Username or Password.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error");
            }

            var tasks = _toDoListItemService.GetToDoListItems().Where(f => f.UserId == authenticatedUser).ToList();
            return View(tasks);
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error");
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddNewToDoListItem(string myTaskTitle, string myTaskDescription)
        {
            try
            {
                ViewBag.myTaskTitle = myTaskTitle;
                ViewBag.myTaskDescription = myTaskDescription;

                if (ViewBag.myTaskTitle == null)
                {
                    ViewBag.ToDoItemMessage = "ToDoItem not added";
                    return RedirectToAction("Index", "Home");
                }

                _toDoListItemService.AddToDoListItem(myTaskTitle, myTaskDescription);
                ViewBag.ToDoItemMessage = "ToDoItem succesfully added.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CompleteToDoListItem([FromBody] string taskId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(taskId))
                {
                    var tasks = _toDoListItemService.CompleteToDoListItem(taskId);
                    return Json(new { success = true, message = "Task successfully completed." });
                }
                else
                {
                    return Json(new { success = false, message = "Task not completed." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult DeleteToDoListItem([FromBody] string taskId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(taskId))
                {
                    var tasks = _toDoListItemService.RemoveToDoListItem(taskId);
                    return Json(new { success = true, message = "Task successfully deleted." });
                }
                else
                {
                    return Json(new { success = false, message = "Task not deleted." });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Error");
            }

            return View();
        }
    }
}
