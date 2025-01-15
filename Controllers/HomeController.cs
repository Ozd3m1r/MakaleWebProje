using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IServiceManager _manager;

    public HomeController(ILogger<HomeController> logger, IServiceManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    public async Task<IActionResult> Index()
    {
        var homeMakale = await _manager.MakaleServices.GetMakaleIsShowHomeAsync(false);
        var filteredMakale = homeMakale.Where(m => m.MakaleIsShowHome && m.MakaleIsShow);
        return View(filteredMakale);
    }

    public IActionResult Search(string searchTerm, int? categoryId)
    {
        var parameters = new MakaleRequestParameters
        {
            SearchTerm = searchTerm,
            CategoryId = categoryId
        };

        // Makale/Index'e yönlendirme yaparken parametreleri QueryString olarak gönder
        if (!string.IsNullOrEmpty(searchTerm) || categoryId.HasValue)
        {
            return RedirectToAction("Index", "Makale", new { 
                SearchTerm = searchTerm, 
                CategoryId = categoryId 
            });
        }

        // Eğer arama parametresi yoksa ana sayfaya dön
        return RedirectToAction("Index");
    } 
} 