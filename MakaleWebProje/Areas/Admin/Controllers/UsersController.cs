using Microsoft.AspNetCore.Mvc;

namespace MakaleWebProje.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
