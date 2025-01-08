using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;

namespace StoreApp.Components
{
    public class MakaleCarousel: ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public MakaleCarousel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IViewComponentResult Invoke()
        {
            var makale = _serviceManager.MakaleServices.GetMakaleCarousel(false);
            return View(makale);
        }
    }
}
