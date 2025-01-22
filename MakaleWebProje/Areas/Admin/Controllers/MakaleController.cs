using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.InterfaceClass;
using Entities.Models;
using Entities.Dtos.MakaleDtos;

namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MakaleController : Controller
    {
        private readonly IServiceManager _manager;
        private const int MaxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public MakaleController(IServiceManager manager)
        {
            _manager = manager;
        }

        #region Ana İşlemler
        public async Task<IActionResult> Index()
        {
            var model = await _manager.MakaleServices.GetAllMakaleAsync(false);
            ViewBag.MakaleLikesDislikes = await GetMakaleLikesDislikesAsync(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View(new MakaleDtoInsertion { MakaleDate = DateTime.Now.ToString("yyyy-MM-dd") });
        }

        [HttpGet]
        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = GetCategories();
            var model = _manager.MakaleServices.GetOneMakaleUpdate(id, false);
            return View(model);
        }
        #endregion

        #region Post İşlemleri
        [HttpPost]
        public async Task<IActionResult> Create(MakaleDtoInsertion makaleDto, IFormFile file)
        {
            try
            {
                LogFormData(makaleDto, file);

                if (!ValidateModel(makaleDto))
                {
                    ViewBag.Categories = GetCategories();
                    return View(makaleDto);
                }

                makaleDto.MakaleDate ??= DateTime.Now.ToString("yyyy-MM-dd");
                makaleDto.MakaleImagesUrl = await ProcessUploadedFile(file) ?? "/images/default.jpg";

                _manager.MakaleServices.CreateMakale(makaleDto);
                TempData["SuccessMessage"] = "Makale başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleError("", $"Bir hata oluştu: {ex.Message}");
                return View(makaleDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] MakaleDtoUpdate makaleDto, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = GetCategories();
                    return View(makaleDto);
                }

                if (file != null)
                {
                    var imageUrl = await ProcessUploadedFile(file);
                    if (imageUrl != null)
                    {
                        await DeleteExistingImage(makaleDto.Id);
                        makaleDto.MakaleImagesUrl = imageUrl;
                    }
                }
                else
                {
                    makaleDto.MakaleImagesUrl = GetExistingImageUrl(makaleDto.Id);
                }

                _manager.MakaleServices.OneUpdateMakale(makaleDto);
                TempData["Message"] = "Makale başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleError("", $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                return View(makaleDto);
            }
        }
        #endregion

        #region Toggle İşlemleri
        [HttpPost]
        public IActionResult ToggleShowHome(int id) => ToggleMakaleProperty(id,
            (makale) => makale.MakaleIsShowHome = !makale.MakaleIsShowHome,
            (makale) => makale.MakaleIsShowHome);

        [HttpPost]
        public IActionResult ToggleShowCarrosel(int id) => ToggleMakaleProperty(id,
            (makale) => makale.MakaleCarousel = !makale.MakaleCarousel,
            (makale) => makale.MakaleCarousel);

        [HttpPost]
        public IActionResult ToggleShowMakale(int id) => ToggleMakaleProperty(id,
            (makale) => makale.MakaleIsShow = !makale.MakaleIsShow,
            (makale) => makale.MakaleIsShow);
        #endregion

        #region Yardımcı Metodlar
        private List<Kategori> GetCategories()
            => _manager.KategoriServices.GetAllKategori(false)?.ToList() ?? new List<Kategori>();

        private async Task<Dictionary<int, (int LikesCount, int DislikesCount)>> GetMakaleLikesDislikesAsync(IEnumerable<Makale> makaleler)
        {
            var result = new Dictionary<int, (int LikesCount, int DislikesCount)>();
            foreach (var makale in makaleler)
            {
                var likes = await _manager.MakaleDataServices.GetLikesByMakaleId(makale.MakaleId);
                var dislikes = await _manager.MakaleDataServices.GetDislikeByMakaleId(makale.MakaleId);
                result[makale.MakaleId] = (likes, dislikes);
            }
            return result;
        }

        private async Task<string> ProcessUploadedFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            if (!ValidateFile(file)) return null;

            var (fileName, filePath) = GenerateFilePathInfo(file);
            await SaveFile(file, filePath);
            return fileName;
        }

        private bool ValidateFile(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                HandleError("file", "Sadece .jpg, .jpeg, .png ve .gif uzantılı dosyalar yüklenebilir.");
                return false;
            }

            if (file.Length > MaxFileSize)
            {
                HandleError("file", "Dosya boyutu 5MB'dan büyük olamaz.");
                return false;
            }

            return true;
        }

        private (string fileName, string filePath) GenerateFilePathInfo(IFormFile file)
        {
            var uniqueFileName = $"{DateTime.Now:yyyyMMdd}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString("00");
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "makale", year, month);

            Directory.CreateDirectory(uploadsFolder);

            return (
                $"/images/makale/{year}/{month}/{uniqueFileName}",
                Path.Combine(uploadsFolder, uniqueFileName)
            );
        }

        private async Task SaveFile(IFormFile file, string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        private async Task DeleteExistingImage(int makaleId)
        {
            var existingMakale = _manager.MakaleServices.GetOneMakale(makaleId, false);
            if (existingMakale?.MakaleImagesUrl != null)
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    existingMakale.MakaleImagesUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
        }

        private string GetExistingImageUrl(int makaleId)
        {
            var existingMakale = _manager.MakaleServices.GetOneMakale(makaleId, false);
            return existingMakale?.MakaleImagesUrl;
        }

        private IActionResult ToggleMakaleProperty(int id, Action<Makale> toggleAction, Func<Makale, bool> getProperty)
        {
            try
            {
                var makale = _manager.MakaleServices.GetOneMakale(id, true);
                if (makale == null)
                    return Json(new { success = false, message = "Makale bulunamadı." });

                toggleAction(makale);
                _manager.MakaleServices.UpdateMakale(makale);
                return Json(new { success = true, isToggled = getProperty(makale) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private void HandleError(string key, string message)
        {
            ModelState.AddModelError(key, message);
            ViewBag.Categories = GetCategories();
        }

        private void LogFormData(MakaleDtoInsertion makaleDto, IFormFile file)
        {
            System.Diagnostics.Debug.WriteLine($"MakaleName: {makaleDto.MakaleName}");
            System.Diagnostics.Debug.WriteLine($"MakaleSummary: {makaleDto.MakaleSummary}");
            System.Diagnostics.Debug.WriteLine($"KategoriId: {makaleDto.KategoriId}");
            System.Diagnostics.Debug.WriteLine($"MakaleDate: {makaleDto.MakaleDate}");
            System.Diagnostics.Debug.WriteLine($"File: {(file != null ? file.FileName : "null")}");
        }

        private bool ValidateModel(MakaleDtoInsertion makaleDto)
        {
            if (makaleDto == null)
            {
                HandleError("", "Form verileri boş geldi!");
                return false;
            }
            makaleDto.MakaleDate = DateTime.Now.ToString();
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                    }
                }
                return false;
            }

            return true;
        }
        #endregion
    }
}