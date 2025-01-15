using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;
using Entities.Models;

namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleServices _userRoleServices;

        public UserRoleController(IUserRoleServices userRoleServices)
        {
            _userRoleServices = userRoleServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _userRoleServices.GetAllUserRoles();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _userRoleServices.CreateUserRole(userRole);
                return RedirectToAction("Index");
            }
            return View(userRole);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = _userRoleServices.GetUserRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _userRoleServices.UpdateUserRole(userRole);
                return RedirectToAction("Index");
            }
            return View(userRole);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRoleServices.DeleteUserRole(id);
            return RedirectToAction("Index");
        }
    }
}