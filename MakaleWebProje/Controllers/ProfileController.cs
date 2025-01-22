using Entities.Dtos.UserDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Lütfen tüm alanları doğru şekilde doldurun." });
                }

                var user = _manager.UsersServices.GetUserById(model.Id, trackChanges: true);
                if (user == null)
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });

                // Oturum kontrolü
                var sessionUserId = HttpContext.Session.GetInt32("UserId");
                if (sessionUserId != model.Id)
                {
                    return Json(new { success = false, message = "Bu işlem için yetkiniz bulunmamaktadır." });
                }

                // Boş alan kontrolü
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.SurName) ||
                    string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.UserName))
                {
                    return Json(new { success = false, message = "Lütfen tüm alanları doldurun." });
                }

                // Email formatı kontrolü
                //if (!IsValidEmail(model.Email))
                //{
                //    return Json(new { success = false, message = "Lütfen geçerli bir email adresi girin." });
                //}


                // Email kontrolü
                if (user.Email != model.Email)
                {
                    var existingEmail = _manager.UsersServices.GetByEmail(model.Email, false);
                    if (existingEmail != null)
                    {
                        return Json(new { success = false, message = "Bu email adresi başka bir kullanıcı tarafından kullanılıyor." });
                    }
                }

                // Kullanıcı adı kontrolü
                if (user.UserName != model.UserName)
                {
                    var existingUserName = _manager.UsersServices.GetByUserName(model.UserName, false);
                    if (existingUserName != null)
                    {
                        return Json(new { success = false, message = "Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor." });
                    }
                }

                // Şifre kontrolü
                if (string.IsNullOrEmpty(model.CurrentPassword))
                {
                    return Json(new { success = false, message = "Lütfen mevcut şifrenizi girin." });
                }

                var currentPasswordHash = HashPassword(model.CurrentPassword);
                if (user.Password != currentPasswordHash)
                {
                    return Json(new { success = false, message = "Mevcut şifreniz yanlış. Lütfen kontrol edip tekrar deneyin." });
                }

                // UpdateUserChangeProfile metodunu try-catch içinde çağır
                try
                {
                    _manager.UsersServices.UpdateUserChangeProfile(model);
                    return Json(new
                    {
                        success = true,
                        message = "Profiliniz başarıyla güncellendi. Yönlendiriliyorsunuz..."
                    });
                }
                catch (Exception)
                {
                    // Benzersizlik hatası durumunda
                    var checkUserName = _manager.UsersServices.GetByUserName(model.UserName, false);
                    if (checkUserName != null && checkUserName.UserId != model.Id)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor."
                        });
                    }

                    var checkEmail = _manager.UsersServices.GetByEmail(model.Email, false);
                    if (checkEmail != null && checkEmail.UserId != model.Id)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Bu email adresi başka bir kullanıcı tarafından kullanılıyor."
                        });
                    }

                    // Diğer hatalar için
                    return Json(new
                    {
                        success = false,
                        message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin."
                    });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Güncelleme işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin."
                });
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Lütfen tüm alanları doldurun." });
                }

                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return Json(new { success = false, message = "Oturum süreniz dolmuş. Lütfen tekrar giriş yapın." });
                }

                var user = _manager.UsersServices.GetUserById(userId.Value, trackChanges: false);
                if (user == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }

                // Mevcut şifre kontrolü
                var currentPasswordHash = _manager.UsersServices.HashPassword(model.CurrentPassword);
                if (user.Password != currentPasswordHash)
                {
                    return Json(new { success = false, message = "Mevcut şifreniz yanlış." });
                }

                // Yeni şifre kontrolü
                if (model.NewPassword != model.ConfirmPassword)
                {
                    return Json(new { success = false, message = "Yeni şifreler eşleşmiyor." });
                }

                // Şifre güncelleme
                var userDtoUpdate = new UserDtoUpdate
                {
                    Id = user.UserId,
                    Name = user.Name,
                    SurName = user.SurName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = model.NewPassword,
                    UserRoleId = user.UserRoleId
                };

                _manager.UsersServices.UpdateUser(userDtoUpdate);

                return Json(new
                {
                    success = true,
                    message = "Şifreniz başarıyla güncellendi. Yönlendiriliyorsunuz..."
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Şifre güncelleme işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin."
                });
            }
        }
    }
}

