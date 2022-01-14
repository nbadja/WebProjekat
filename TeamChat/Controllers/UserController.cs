using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeamChat.Models;

namespace TeamChat.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }   
         
        public IActionResult Signup()
        {
            return View();
        }   

        public IActionResult LoginUser()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }   

        public IActionResult RegisterUser(User user)
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
