using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    public class MakaleController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> ToggleShowHome(int id)
        {
            var makale = _manager.MakaleServices.GetOneMakale(id, true);
            if (makale != null)
            {
                makale.MakaleIsShowHome = !makale.MakaleIsShowHome;
                _manager.MakaleServices.UpdateMakale(makale);
                return Json(new { success = true, isShowHome = makale.MakaleIsShowHome });
            }
            return Json(new { success = false });
        }
    }
} 