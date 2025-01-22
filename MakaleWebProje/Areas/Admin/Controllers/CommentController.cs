using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;

namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly int _pageSize = 10; // Her sayfada 10 yorum

        public CommentController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(int page = 1)
        {
            var comments = _manager.MakaleCommentServices
                .GetAllMakaleComment(false)
                .OrderByDescending(c => c.CreatedDate)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            var totalComments = _manager.MakaleCommentServices.GetAllMakaleComment(false).Count();
            var totalPages = (int)Math.Ceiling(totalComments / (double)_pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(comments);
        }

        public IActionResult Delete(int id)
        {
            var comment = _manager.MakaleCommentServices.GetCommentById(id, true);
            if (comment != null)
            {
                _manager.MakaleCommentServices.DeleteComment(id, true);
                TempData["Message"] = "Yorum başarıyla silindi.";
            }
            return RedirectToAction("Index");
        }
    }
}
