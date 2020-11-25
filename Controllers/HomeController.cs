using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task2.Models;
using Task2.Repository;
using Task2.Repository.IRepository;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepo;


        public HomeController(ILogger<HomeController> logger, IUserRepository userRepo)
       
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            //return RedirectToAction("Index", "Image");
            //return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpGet]
        public UserModel/*IActionResult*/ Login()
        {
            UserModel obj = new UserModel();
            obj.Choice = _userRepo.Fetch();
            return obj;
            /*return View(obj);*/
        }

        [HttpPost]
        public /*IActionResult*/Object Login(UserModel obj)
        {
            var user = _userRepo.Authenticate(obj.Email, obj.Password);
            return user;
            /*if (user == null)
            {
                return RedirectToAction("Login", "Home");
                //return BadRequest(new { message = "Username or password is incorrect" });
            }*/

            //return Ok(user);
            /* return RedirectToAction("Index","Image");*/
        }

        public IActionResult Privacy()
        {
           return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
