using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;
using Entities.Models;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;

namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersServices _userServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IServiceManager _manager;

        public UsersController(IUsersServices userServices, IUserRoleServices userRoleServices, IServiceManager manager)
        {
            _userServices = userServices;
            _userRoleServices = userRoleServices;
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userServices.GetAllUsers(false);

            return View(users);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userServices.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AssignRole(int userId)
        {
            var user = _userServices.GetUserById(userId,false);
            var roles = _userRoleServices.GetAllUserRoles();
            ViewBag.Roles = roles;
            return View(user);
        }

        [HttpPost]
        public IActionResult AssignRole(int userId, int roleId)
        {
            _userServices.AssignRoleToUser(userId, roleId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var userDto = _userServices.GetOneUserUpdate(id, false);
            if (userDto == null)
                return NotFound();

            var roles = _userRoleServices.GetAllUserRoles();
            ViewBag.Roles = roles;

            return View(userDto);
        }

        [HttpPost]
        public IActionResult Update(UserDtoUpdate userDto)
        {
            var password = _userServices.GetUserById(userDto.Id, false).Password;
            userDto.Password = password;
            if (ModelState.IsValid)
            {
                try
                {
                    _userServices.UpdateUserChangeProfile(userDto);
                    TempData["Message"] = "Kullanıcı başarıyla güncellendi.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Güncelleme sırasında hata: {ex.Message}");
                }
            }

            // Hata durumunda rolleri tekrar yükle
            var roles = _userRoleServices.GetAllUserRoles();
            ViewBag.Roles = roles;
            return View(userDto);
        }

    }
}