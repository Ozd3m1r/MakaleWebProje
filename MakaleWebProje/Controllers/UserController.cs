using Entities.Dtos.LoginDtos;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MakaleWebProje.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;


        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var users = _manager.UsersServices.GetAllUsers(true);
            return View(users);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([FromForm] UserDtoInsertion userDto, string confirmPassword)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Lütfen tüm alanları doldurun." });
                }

                if (userDto.Password != confirmPassword)
                {
                    return Json(new { success = false, message = "Şifreler eşleşmiyor." });
                }

                // Email veya kullanıcı adı kontrolü
                var existingUser = _manager.UsersServices.GetByUserName(userDto.UserName);
                if (existingUser != null)
                {
                    return Json(new { success = false, message = "Bu kullanıcı adı zaten kullanılıyor." });
                }

                existingUser = _manager.UsersServices.GetByEmail(userDto.Email);
                if (existingUser != null)
                {
                    return Json(new { success = false, message = "Bu email adresi zaten kullanılıyor." });
                }

                // Varsayılan olarak User rolü atama
                userDto.UserRoleId = 1; // User rolü ID'si

                _manager.UsersServices.CreateUser(userDto);

                return Json(new
                {
                    success = true,
                    message = "Kayıt işlemi başarıyla tamamlandı. Giriş sayfasına yönlendiriliyorsunuz."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Kayıt işlemi sırasında bir hata oluştu: " + ex.Message
                });
            }
        }
        public IActionResult SweetAlertDemo()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Eğer oturumda "UserId" varsa, doğrudan ProfileDetails'a yönlendir
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
               return RedirectToAction("Index", "Home");
            }

            // Oturum boşsa giriş sayfasını göster
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Lütfen tüm alanları doldurun." });
                }

                var user = _manager.UsersServices.GetAllUsers(trackChanges: true)
                                               .FirstOrDefault(u => u.UserName == loginDto.UserName);

                if (user == null)
                {
                    return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre." });
                }

                bool isPasswordValid = _manager.UsersServices.VerifyPassword(user.UserId, loginDto.Password);
                if (!isPasswordValid)
                {
                    return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre." });
                }

                // Kullanıcı ID'sini oturuma kaydet
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetInt32("UserRoleId", user.UserRoleId);

                var userRoleName = user.UserRoleId == 1 ? "User" : "Admin";

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, userRoleName)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Json(new { success = true, message = "Giriş başarılı! Yönlendiriliyorsunuz..." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }




    }
}
