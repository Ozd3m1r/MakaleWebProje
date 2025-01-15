using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.InterfaceClass;
using MakaleWebProje.Areas.Admin.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Entities.Models;
using Entities.Dtos.MakaleDtos;
using Microsoft.AspNetCore.Authorization;
namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MakaleController : Controller
    {
        private readonly IServiceManager _manager;

        public MakaleController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            // Asenkron olarak makaleleri al
            var model = await _manager.MakaleServices.GetAllMakaleAsync(false);

            // MakaleId'ye göre beğeni ve beğenmeme sayıları için bir dictionary oluştur
            var makaleLikesDislikes = new Dictionary<int, (int LikesCount, int DislikesCount)>();

            // Her makale için beğeni ve beğenmeme sayısını al
            foreach (var makale in model)
            {
                // Asenkron işlemlerle beğeni ve beğenmeme sayılarını al
                var LikesCount = await _manager.MakaleDataServices.GetLikesByMakaleId(makale.MakaleId);
                var DislikesCount = await _manager.MakaleDataServices.GetDislikeByMakaleId(makale.MakaleId);

                // Sayıları dictionary'ye ekle
                makaleLikesDislikes[makale.MakaleId] = (LikesCount, DislikesCount);
            }

            // Verileri ViewBag üzerinden view'a gönder
            ViewBag.MakaleLikesDislikes = makaleLikesDislikes;

            // Makale verilerini view'a gönder
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
            ViewBag.Categories = categories ?? new List<Kategori>();
            return View(new MakaleDtoInsertion());
        }


        [HttpPost]
        public async Task<IActionResult> Create(MakaleDtoInsertion makaleDto, IFormFile file)
        {
            try
            {
                // Debug için form verilerini yazdır
                System.Diagnostics.Debug.WriteLine($"MakaleName: {makaleDto.MakaleName}");
                System.Diagnostics.Debug.WriteLine($"MakaleSummary: {makaleDto.MakaleSummary}");
                System.Diagnostics.Debug.WriteLine($"KategoriId: {makaleDto.KategoriId}");
                System.Diagnostics.Debug.WriteLine($"MakaleDate: {makaleDto.MakaleDate}");
                System.Diagnostics.Debug.WriteLine($"File: {(file != null ? file.FileName : "null")}");

                // Form verilerinin null kontrolü
                if (makaleDto == null)
                {
                    ModelState.AddModelError("", "Form verileri boş geldi!");
                    ViewBag.Categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
                    return View(new MakaleDtoInsertion());
                }

                // Tarih kontrolü
                makaleDto.MakaleDate ??= DateTime.Now.ToString("yyyy-MM-dd");

                // Dosya işleme
                if (file != null && file.Length > 0)
                {
                    string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    string filePath = Path.Combine(uploads, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    makaleDto.MakaleImagesUrl = $"/images/{file.FileName}";
                }
                else
                {
                    // Eğer dosya yüklenmediyse varsayılan bir resim atayabilirsiniz
                    makaleDto.MakaleImagesUrl = "/images/default.jpg"; // veya null bırakın
                }

                // Validation kontrolü
                if (!ModelState.IsValid)
                {
                    foreach (var modelState in ModelState)
                    {
                        foreach (var error in modelState.Value.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                        }
                    }

                    ViewBag.Categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
                }

                // Makaleyi kaydet
                _manager.MakaleServices.CreateMakale(makaleDto);
                TempData["SuccessMessage"] = "Makale başarıyla eklendi.";
                return RedirectToAction("Index", "Makale");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                ViewBag.Categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
                return View(makaleDto);
            }
        }



        [HttpGet]
        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            var categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
            ViewBag.Categories = categories ?? new List<Kategori>();

            var model = _manager.MakaleServices.GetOneMakaleUpdate(id, false);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] MakaleDtoUpdate MakaleDto, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Eğer yeni bir dosya yüklendiyse
                    if (file != null && file.Length > 0)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        MakaleDto.MakaleImagesUrl = String.Concat("/images/", file.FileName);
                    }
                    else
                    {
                        // Dosya yüklenmediyse mevcut görseli koru
                        var existingMakale = _manager.MakaleServices.GetOneMakale(MakaleDto.Id, false);
                        if (existingMakale != null)
                        {
                            MakaleDto.MakaleImagesUrl = existingMakale.MakaleImagesUrl;
                        }
                    }

                    // Makaleyi güncelle
                    _manager.MakaleServices.OneUpdateMakale(MakaleDto);
                    TempData["Message"] = "Makale başarıyla güncellendi.";
                    return RedirectToAction("Index");
                }

                // ModelState geçerli değilse
                var categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
                ViewBag.Categories = categories ?? new List<Kategori>();
                return View(MakaleDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                var categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
                ViewBag.Categories = categories ?? new List<Kategori>();
                return View(MakaleDto);
            }
        }
        [HttpPost]
        public IActionResult ToggleShowHome(int id)
        {
            try
            {
                var makale = _manager.MakaleServices.GetOneMakale(id, true);
                if (makale != null)
                {
                    makale.MakaleIsShowHome = !makale.MakaleIsShowHome;
                    _manager.MakaleServices.UpdateMakale(makale); // GetOneMakaleUpdate yerine UpdateMakale kullanıyoruz
                    return Json(new { success = true, isShowHome = makale.MakaleIsShowHome });
                }
                return Json(new { success = false, message = "Makale bulunamadı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ToggleShowCarrosel(int id)
        {
            try
            {
                var makale = _manager.MakaleServices.GetOneMakale(id, true);
                if (makale != null)
                {
                    makale.MakaleCarousel = !makale.MakaleCarousel;
                    _manager.MakaleServices.UpdateMakale(makale);
                    return Json(new { success = true, isShowCarousel = makale.MakaleCarousel });
                }
                return Json(new { success = false, message = "Makale bulunamadı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ToggleShowMakale(int id)
        {
            try
            {
                var makale = _manager.MakaleServices.GetOneMakale(id, true);
                if (makale != null)
                {
                    makale.MakaleIsShow = !makale.MakaleIsShow;
                    _manager.MakaleServices.UpdateMakale(makale);
                    return Json(new { success = true, isShow = makale.MakaleIsShow });
                }
                return Json(new { success = false, message = "Makale bulunamadı." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}

