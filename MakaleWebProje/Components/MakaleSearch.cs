using Microsoft.AspNetCore.Mvc;

namespace MakaleWebProje.Components
{
    public class MakaleSearch : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
