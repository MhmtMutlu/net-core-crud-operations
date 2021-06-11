using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class HomeController : Controller
    {
        // Dependency Injection
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _userService = userService;
            _hostEnvironment = hostEnvironment;
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
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                // Save image file to wwwRoot
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(userModel.ImageFile.FileName);
                string extension = Path.GetExtension(userModel.ImageFile.FileName);
                userModel.Photo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    userModel.ImageFile.CopyTo(fileStream);
                }

                var user = new User()
                {
                    Name = userModel.Name,
                    Surname = userModel.Surname,
                    Email = userModel.Email,
                    BirthDate = userModel.BirthDate,
                    Phone = userModel.Phone,
                    Location = userModel.Location,
                    Photo = userModel.Photo
                };

                _userService.Add(user);

            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int Id)
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

        public IActionResult Delete(int Id)
        {
            var user = _userService.GetById(Id);

            // Delete image file from wwwRoot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", user.Data.Photo);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }


            if (user != null)
            {
                _userService.Delete(user.Data);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(UserModel userModel)
        {
            
            if (ModelState.IsValid)
            {
                var user = _userService.GetById(userModel.Id);

                // Delete old image file from wwwRoot
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", user.Data.Photo);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }


                // Update new image file to wwwRoot
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(userModel.ImageFile.FileName);
                string extension = Path.GetExtension(userModel.ImageFile.FileName);
                userModel.Photo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    userModel.ImageFile.CopyTo(fileStream);
                }

                if (user != null)
                {
                    user.Data.Name = userModel.Name;
                    user.Data.Surname = userModel.Surname;
                    user.Data.Email = userModel.Email;
                    user.Data.BirthDate = userModel.BirthDate;
                    user.Data.Phone = userModel.Phone;
                    user.Data.Location = userModel.Location;
                    user.Data.Photo = userModel.Photo;


                    var result = _userService.Update(user.Data);
                }

            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
