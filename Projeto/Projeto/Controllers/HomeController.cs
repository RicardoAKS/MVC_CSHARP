using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        public IActionResult Index()
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
                ViewBag.UserName = user.Name;
            }
            return View();
        }

        [Route("QuemSomos")]
        public IActionResult Quem_somos()
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
                ViewBag.UserName = user.Name;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
