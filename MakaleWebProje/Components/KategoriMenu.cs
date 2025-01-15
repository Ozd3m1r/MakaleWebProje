using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;

namespace MakaleWebProje.Components
{
    public class KategoriMenu:ViewComponent
    {
        private readonly IServiceManager _manager;

        public KategoriMenu(IServiceManager manager)
        {
            _manager = manager;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _manager.KategoriServices.GetAllKategori(false);
            return View(categories);
        }
    }
}
