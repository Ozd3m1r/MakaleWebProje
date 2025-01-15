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
        public IActionResult Register(UserDtoInsertion userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userDto);
                }
                userDto.UserRoleId = 1;
                _manager.UsersServices.CreateUser(userDto);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userDto);
            }
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
                    return View(loginDto);
                }

                var user = _manager.UsersServices.GetAllUsers(trackChanges: true)
                                               .FirstOrDefault(u => u.UserName == loginDto.UserName );
                bool isPasswordValid = _manager.UsersServices.VerifyPassword(user.UserId, loginDto.Password);
                if (!isPasswordValid)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(loginDto);
                }

                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(loginDto);
                }

                // Kullanıcı ID'sini oturuma kaydet
                HttpContext.Session.SetInt32("UserId", user.UserId);
                ViewData["UserId"] = user.UserId;

                HttpContext.Session.SetInt32("UserRoleId", user.UserRoleId);

                var userRoleName = user.UserRoleId == 1 ? "User" : "Admin"; // Eğer 1 User rolü, diğer Admin

                // Kullanıcı rolünü Claim olarak ekliyoruz
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, userRoleName) // Burada manuel olarak rolü ekliyoruz
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginDto);
            }
        }




    }
}
