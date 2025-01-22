using Entities.Models;
using Entity.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;
using System.Diagnostics;

namespace MakaleWebProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager _manager;

        public HomeController(ILogger<HomeController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public  IActionResult Index()
        {
            var homeMakale =  _manager.MakaleServices.GetMakaleIsShowHome(false);
            return View(homeMakale);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new  { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult About()
        {
            return View();
        }
      
    }
}
