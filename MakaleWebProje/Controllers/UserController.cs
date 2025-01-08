using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;
using Services.InterfaceClass;

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
                return RedirectToAction("ProfileDetails", "Profile");
            }

            // Oturum boşsa giriş sayfasını göster
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(loginDto);
                }
                
                var user = _manager.UsersServices.GetAllUsers(trackChanges: true)
                                       .FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == loginDto.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(loginDto);
                }

                // Kullanıcı ID'sini oturuma kaydet
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("ProfileDetails", "Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginDto);
            }

        }
    }
}
