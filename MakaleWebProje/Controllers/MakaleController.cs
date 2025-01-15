using Entities.Models;
using Entity.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;
using Services.InterfaceClass;

namespace MakaleWebProje.Controllers
{
    public class MakaleController : Controller
    {
        private readonly IServiceManager _manager;

        public MakaleController(IServiceManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public IActionResult Index(MakaleRequestParameters m)
        {
            var model = _manager.MakaleServices.GetAllMakaleDetails(m)
                .Where(makale => makale.MakaleIsShow || User.IsInRole("Admin")); // Admin tüm makaleleri görebilir

            if (model == null)
            {
                model = new List<Makale>();
            }
            return View(model);
        }
        //public async Task<IActionResult> Get(int id)
        //{
        //    // Makale verisini alıyoruz
        //    var makale =  _manager.MakaleServices.GetOneMakale(id, false);
        //    if (makale == null)
        //    {
        //        return NotFound();
        //    }

        //    // Beğeni ve beğenmeme sayılarını alıyoruz
        //    var likesCount = await _manager.MakaleDataServices.GetLikesByMakaleId(id);
        //    var dislikesCount = await _manager.MakaleDataServices.GetDislikeByMakaleId(id);

        //    // Sayıları ViewBag ile View'a gönderiyoruz
        //    ViewBag.LikesCount = likesCount;
        //    ViewBag.DislikesCount = dislikesCount;

        //    // Kullanıcının giriş yapıp yapmadığını kontrol ediyoruz
        //    var userId = HttpContext.Session.GetInt32("UserId");

        //    // Eğer kullanıcı giriş yapmamışsa, butonları devre dışı bırakıyoruz
        //    if (userId == null)
        //    {
        //        ViewBag.IsUserLoggedIn = false;
        //    }
        //    else
        //    {
        //        ViewBag.IsUserLoggedIn = true;
        //    }
        //    var comments = _manager.MakaleCommentServices.GetActiveCommentsByMakaleId(id, false).ToList();
        //    ViewBag.Comments = comments;

        //    return View(makale);

        //    // Makale modelini View'a gönderiyoruz
        //    return View(makale);
        //}
        /*  public async Task<IActionResult> Get(int id)
          {
              // Makale verisini alıyoruz
              var makale = _manager.MakaleServices.GetOneMakale(id, false);
              if (makale == null || !makale.MakaleIsShow) // MakaleIsShow kontrolü eklendi
              {
                  return NotFound("Makale bulunamadı veya yayında değil.");
              }

              // Beğeni ve beğenmeme sayılarını alıyoruz
              var likesCount = await _manager.MakaleDataServices.GetLikesByMakaleId(id);
              var dislikesCount = await _manager.MakaleDataServices.GetDislikeByMakaleId(id);

              // Admin kontrolü
              bool isAdmin = User.IsInRole("Admin");

              // Eğer makale yayında değilse ve kullanıcı admin değilse erişimi engelle
              if (!makale.MakaleIsShow && !isAdmin)
              {
                  return RedirectToAction("Index", "Home");
              }

              // ViewBag değerlerini set ediyoruz
              ViewBag.LikesCount = likesCount;
              ViewBag.DislikesCount = dislikesCount;
              ViewBag.IsUserLoggedIn = HttpContext.Session.GetInt32("UserId") != null;
              ViewBag.Comments = _manager.MakaleCommentServices.GetActiveCommentsByMakaleId(id, false).ToList();

              // Eğer makale yayında değilse ve admin kullanıcısı görüntülüyorsa uyarı mesajı göster
              if (!makale.MakaleIsShow && isAdmin)
              {
                  TempData["Warning"] = "Bu makale şu anda yayında değil. Sadece admin kullanıcıları görüntüleyebilir.";
              }

              return View(makale);
          }*/
        public async Task<IActionResult> Get(int id)
        {

            // Makale verisini alıyoruz
            var makale = _manager.MakaleServices.GetOneMakale(id, false);

            // Makale null ise veya yayında değilse (admin değilse) erişimi engelle
            if (makale == null || (!makale.MakaleIsShow && !User.IsInRole("Admin")))
            {
                TempData["Error"] = "Makale bulunamadı veya erişim yetkiniz yok.";
                return RedirectToAction("Index", "Home");
            }

            // Admin için uyarı mesajı
            if (!makale.MakaleIsShow && User.IsInRole("Admin"))
            {
                TempData["Warning"] = "Bu makale şu anda yayında değil. Sadece admin olarak görüntüleyebilirsiniz.";
            }

            // ViewBag değerlerini set ediyoruz
            ViewBag.LikesCount = await _manager.MakaleDataServices.GetLikesByMakaleId(id);
            ViewBag.DislikesCount = await _manager.MakaleDataServices.GetDislikeByMakaleId(id);
            ViewBag.IsUserLoggedIn = HttpContext.Session.GetInt32("UserId") != null;
            ViewBag.Comments = _manager.MakaleCommentServices.GetActiveCommentsByMakaleId(id, false).ToList();

            return View(makale);
        }
        [HttpPost]
        public async Task<IActionResult> OnPostLike(int MakaleId, string returnUrl)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var makale =  _manager.MakaleServices.GetOneMakale(MakaleId, false);
            if (makale != null)
            {
                // Beğeni işlemi yapılır ve kullanıcı ID'si kaydedilir
                await _manager.MakaleDataServices.AddLike((int)userId, MakaleId);
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostDislike(int MakaleId, string returnUrl)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var makale =  _manager.MakaleServices.GetOneMakale(MakaleId, false);
            if (makale != null)
            {
                // Beğenmeme işlemi yapılır ve kullanıcı ID'si kaydedilir
                await _manager.MakaleDataServices.AddDislike((int)userId, MakaleId);
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult AddComment(int makaleId, string comment)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var newComment = new MakaleComment
            {
                MakaleId = makaleId,
                UserId = (int)userId,
                Comment = comment,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _manager.MakaleCommentServices.AddComment(newComment);

            return RedirectToAction("Get", new { id = makaleId });
        }

        [HttpPost]
        public IActionResult DeleteComment(int commentId, int makaleId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var comment = _manager.MakaleCommentServices.GetCommentById(commentId, true);
            if (comment != null && comment.UserId == userId)
            {
                _manager.MakaleCommentServices.DeleteComment(commentId, true);
            }

            return RedirectToAction("Get", new { id = makaleId });
        }

    }
}
