using Microsoft.AspNetCore.Mvc;

namespace MakaleWebProje.Area.Admin.Controllers
    
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
