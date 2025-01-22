using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;

namespace MakaleWebProje.Area.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IServiceManager _manager;

        public AdminHomeController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            // İstatistikleri al
            var totalMakale = _manager.MakaleServices.GetAllMakale(false);
            var totalUsers = _manager.UsersServices.GetAllUsers(false);
            var totalComments = _manager.MakaleCommentServices.GetAllMakaleComment(false);

            // ViewBag ile view'a gönder
            ViewBag.MakaleCount = totalMakale.Count();
            ViewBag.UserCount = totalUsers.Count();
            ViewBag.CommentCount = totalComments.Count();

            return View();
        }
    }
}