using System.Diagnostics;
//using Identity_YuQin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity_YuQin.Controllers
{
    public class HomeIdentity_YuQinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /**
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        **/
    }
}
