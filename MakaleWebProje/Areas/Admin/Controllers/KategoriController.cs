using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.InterfaceClass;

namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KategoriController : Controller
    {
        private readonly IKategoriServices _kategoriServices;

        public KategoriController(IKategoriServices kategoriServices)
        {
            _kategoriServices = kategoriServices;
        }

        // Kategori listeleme (Admin)
        public IActionResult Index()
        {
            var kategoriler = _kategoriServices.GetAllKategori(trackChanges: false);
            return View(kategoriler);
        }

        // Yeni kategori ekleme sayfası
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Kategori ekleme işlemi
        [HttpPost]
        public IActionResult Create(Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                return View(kategori);
            }

            _kategoriServices.CreateKategori(kategori);
            TempData["Message"] = "Kategori başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        // Kategori düzenleme sayfası (Admin)
        [HttpGet]
        public IActionResult Update(int id)
        {
            var kategori = _kategoriServices.GetKategoriById(id, trackChanges: false);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // Kategori düzenleme işlemi
        [HttpPost]
        public IActionResult Update(Kategori kategori)
        {
            if (!ModelState.IsValid)
            {
                return View(kategori);
            }

            _kategoriServices.UpdateKategori(kategori);
            TempData["Message"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        // Kategori silme işlemi (Admin)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var kategori = _kategoriServices.GetKategoriById(id, trackChanges: false);
            if (kategori == null)
            {
                return NotFound();
            }

            _kategoriServices.DeleteKategori(kategori);
            TempData["Message"] = "Kategori başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}

