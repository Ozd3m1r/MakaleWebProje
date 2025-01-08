using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;
using Services.InterfaceClass;

namespace MakaleWebProje.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IServiceManager _manager;

        public ProfileController(IServiceManager manager)
        {
            _manager = manager;
        }

        
        public IActionResult ProfileDetails()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _manager.UsersServices.GetUserById(userId.Value, trackChanges: false);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return View(user);
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Update()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _manager.UsersServices.GetUserById(userId.Value, trackChanges: false);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(UserDtoUpdate userDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userDto);
            }

            try
            {
                _manager.UsersServices.UpdateUser(userDto);
                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userDto);
            }
        }
        [HttpPost]
        public IActionResult Delete()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("index", "home");
            }

            try
            {
                _manager.UsersServices.DeleteUser(userId.Value);

                // Profil silindiğinde çıkış yap ve giriş sayfasına yönlendir
                HttpContext.Session.Clear();
                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Details");
            }
        }
    
    public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Oturum temizleme
            return RedirectToAction("index","home");
        }
    }
}
