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
            var model = _manager.MakaleServices.GetAllMakale(true);
            return View(model);
        }
        public async Task<IActionResult> Get(int id)
        {
            // Makale verisini alıyoruz
            var makale =  _manager.MakaleServices.GetOneMakale(id, false);
            if (makale == null)
            {
                return NotFound();
            }

            // Beğeni ve beğenmeme sayılarını alıyoruz
            var likesCount = await _manager.MakaleDataServices.GetLikesByMakaleId(id);
            var dislikesCount = await _manager.MakaleDataServices.GetDislikeByMakaleId(id);

            // Sayıları ViewBag ile View'a gönderiyoruz
            ViewBag.LikesCount = likesCount;
            ViewBag.DislikesCount = dislikesCount;

            // Kullanıcının giriş yapıp yapmadığını kontrol ediyoruz
            var userId = HttpContext.Session.GetInt32("UserId");

            // Eğer kullanıcı giriş yapmamışsa, butonları devre dışı bırakıyoruz
            if (userId == null)
            {
                ViewBag.IsUserLoggedIn = false;
            }
            else
            {
                ViewBag.IsUserLoggedIn = true;
            }

            // Makale modelini View'a gönderiyoruz
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


    }
}
