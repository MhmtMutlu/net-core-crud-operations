using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Dependency Injection
        IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Used UserViewModel to hold users in a list format
            var userViewModel = new UserViewModel()
            {
                Users = _userService.GetAll().Data
            };
            
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult User(int Id)
        {
            var user = _userService.GetById(Id);
            var model = new UserModel
            {
                Id = user.Data.Id,
                Name = user.Data.Name,
                Surname = user.Data.Surname,
                Email = user.Data.Email,
                BirthDate = user.Data.BirthDate,
                Phone = user.Data.Phone,
                Location = user.Data.Location,
                Photo = user.Data.Photo
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var user = _userService.GetById(Id);

            _userService.Delete(user.Data);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
