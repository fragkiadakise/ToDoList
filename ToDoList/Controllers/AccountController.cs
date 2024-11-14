using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {

        private readonly IMockAuthService _mockAuthService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMockAuthService mockAuthService, ILogger<AccountController> logger)
        {
            _mockAuthService = mockAuthService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = _mockAuthService.AuthenticateUser(model.Email, model.Password);

                    if (user != null)
                    {
                        HttpContext.Session.SetString("AuthenticateUser", user.Id);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return View("Error", model);
            }

            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        var errors = _mockAuthService.RegisterUser(model);
                        if (!string.IsNullOrWhiteSpace(errors))
                        {
                            ViewBag.ErrorMessage = errors;
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
            }

            return RedirectToAction("Login", "Account");
        }


    }
}
