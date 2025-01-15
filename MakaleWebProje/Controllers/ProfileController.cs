using Entities.Dtos.UserDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System.Text;
using System.Security.Cryptography;

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
            var userRoleId = HttpContext.Session.GetInt32("UserRoleId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _manager.UsersServices.GetUserById(userId.Value, trackChanges: false);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            // UserRoleId'yi kullanarak UserRole bilgisini çekiyoruz
           // var userRole = _manager.UserRoleServices.GetUserRoleById(userRoleId.Value);
            var userRole=_manager.UserRoleServices.GetUserRoleNameById(userRoleId.Value);
            // Rol bilgisini ViewBag'e aktarıyoruz
            ViewBag.UserRole = userRole; // null kontrolü ile rolü alıyoruz
            return View(user);
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Update()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _manager.UsersServices.GetUserById(userId.Value, trackChanges: false);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var userDto = new UserDtoUpdate
            {
                Id = user.UserId,
                Name = user.Name,
                SurName = user.SurName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password, // Mevcut şifreyi hidden field için saklıyoruz
                UserRoleId = user.UserRoleId
            };

            return View(userDto);
        }

        [HttpPost]
        public IActionResult Update(UserDtoUpdate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _manager.UsersServices.GetUserById(model.Id, trackChanges: true);
            if (user == null)
                return NotFound();

            // Oturum kontrolü
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId != model.Id)
            {
                return Unauthorized("Bu işlem için yetkiniz yok.");
            }

            // Şifre kontrolü (Mevcut şifreyi hash'leyerek kontrol ediyoruz)
            if (sessionUserId is int)
            {
                var currentPasswordHash = _manager.UsersServices.GetUserById(sessionUserId.Value, false).Password;
                if (currentPasswordHash != HashPassword(model.CurrentPassword)) // SHA-256 ile hash'lenmiş şifreyi kontrol ediyoruz
                {
                    ModelState.AddModelError("CurrentPassword", "Mevcut şifre yanlış");
                    return View(model);
                }
            }

            try
            {
                // Şifre doğrulandıysa, değişiklikleri uygula

                _manager.UsersServices.UpdateUserChangeProfile(model);

                TempData["Message"] = "Profiliniz başarıyla güncellendi.";
                return RedirectToAction("ProfileDetails", "Profile");
            }
            catch (Exception ex)
            {
                // Log the error
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
                return View(model);
            }
        }

        // SHA-256 ile şifreyi hash'leme metodu
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Hexadecimal formatta yazdırıyoruz
                }
                return sb.ToString(); // Hashlenmiş şifreyi döndürüyoruz
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
       // [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Oturum temizleme
            return RedirectToAction("index","home");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Şifre değiştirme formu için kullanıcıyı gönderecek
            return View();
        }

        // Şifre değiştirme işlemi POST isteği
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var user = _manager.UsersServices.GetUserById(userId.Value, trackChanges: false);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }
            model.CurrentPassword = _manager.UsersServices.HashPassword(model.CurrentPassword);
            // Şifre doğrulama (SHA-256 ile hash kontrolü)
            if (user.Password != model.CurrentPassword)
            {
                Console.WriteLine("Girilmiş şifre: " + model.CurrentPassword);
                Console.WriteLine("Veritabanındaki şifre (hash): " + user.Password);
                ModelState.AddModelError("CurrentPassword", "Mevcut şifre yanlış.");
                return View(model);
            }

            // Yeni şifrelerin eşleşip eşleşmediğini kontrol et
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Yeni şifreler eşleşmiyor.");
                return View(model);
            }

            //model.NewPassword = _manager.UsersServices.HashPassword(model.NewPassword);
            // Yeni şifreyi hash'leyerek güncelliyoruz
            var userDtoUpdate = new UserDtoUpdate
            {
                Id = user.UserId,
                Name = user.Name,
                SurName = user.SurName,
                UserName = user.UserName,
                Email = user.Email,
                Password = model.NewPassword, // Yeni şifreyi burada hash'liyoruz
                UserRoleId = user.UserRoleId
            };

            // Şifreyi güncelle
            _manager.UsersServices.UpdateUser(userDtoUpdate);

            TempData["Message"] = "Şifreniz başarıyla güncellenmiştir.";
            return RedirectToAction("ProfileDetails", "Profile");
            }
    }
}

