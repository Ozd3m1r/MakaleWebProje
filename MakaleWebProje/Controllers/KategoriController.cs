using Microsoft.AspNetCore.Mvc;
using Repositories.InterfaceClass;

namespace MakaleWebProje.Controllers
{
    public class KategoriController : Controller
    {
        private readonly IRepositoryManager _manager;

        public KategoriController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var kategori=_manager.Kategori.FindAll(false);
            return View(kategori);
        }
    }
}
