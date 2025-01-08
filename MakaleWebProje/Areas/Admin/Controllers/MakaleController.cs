using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.InterfaceClass;
using MakaleWebProje.Areas.Admin.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Entities.Models;
namespace MakaleWebProje.Areas.Admin.Controllers
{
    [Area("Admin")]
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

                // Eğer tarih boşsa bugünün tarihini ata
                makaleDto.MakaleDate ??= DateTime.Now.ToString("yyyy-MM-dd");

                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = _manager.KategoriServices.GetAllKategori(false)?.ToList();
                    return View(makaleDto);
                }

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

        /* private SelectList KategoriSelectList()
         {
             return new SelectList(
                  _manager.KategoriServices.GetAllKategori(false),
                  "KategoriId",
                  "KategoriName",
                  "1");
         }*/
        /* [HttpGet]
         public IActionResult Create()
         {
             var categoriesQuery = _manager.KategoriServices.GetAllKategori(false); // IQueryable türü
             if (!categoriesQuery.Any())
             {
                 // Kategoriler boşsa hata mesajı göster
                 ViewBag.Categories = new List<Kategori>();
             }
             else
             {
                 ViewBag.Categories = categoriesQuery.ToList();
             }

             return View();
         }

         //[HttpPost]

         /*  public async Task<IActionResult> Create([FromForm] MakaleDtoInsertion MakaleDto, IFormFile file)
           {
               // Dosya yükleme işlemi
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

                   MakaleDto.MakaleImagesUrl = $"/images/{file.FileName}";
               }

               // Tarih bilgisini otomatik olarak güncelle
               MakaleDto.MakaleDate = DateTime.Now.ToString("yyyy-MM-dd"); // Bugünün tarihini atıyoruz

               // Model doğrulama
               if (ModelState.IsValid)
               {
                   _manager.MakaleServices.CreateMakale(MakaleDto); // Makale oluşturuluyor
                   return RedirectToAction("Index");
               }

               // Eğer model geçerli değilse, kategori listesi ile formu tekrar göster
               ViewBag.Kategories = KategoriSelectList();

               // ModelState hatalarını loglayın
               foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
               {
                   Console.WriteLine($"Model Error: {modelError.ErrorMessage}");
               }

               return View(MakaleDto);
           }*/
        /*  [HttpPost]
          public async Task<IActionResult> Create(MakaleDtoInsertion makaleDto, IFormFile file)
          {
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
                  Console.WriteLine("yüklenmedi");
              }
              if (string.IsNullOrEmpty(makaleDto.MakaleDate))
              {
                  makaleDto.MakaleDate = DateTime.Now.ToString("yyyy-MM-dd");
              }

              if (ModelState.IsValid)
              {
                  ViewBag.Categories = KategoriSelectList();
                  return View(makaleDto);
              }
              _manager.MakaleServices.CreateMakale(makaleDto);
              return RedirectToAction("Index");
          }*/


    }
        //[HttpGet]
        //public IActionResult Update([FromRoute(Name = "id")] int id)
        //{
        //    ViewBag.Categories = KategoriSelectList();
        //    var model = _manager.MakaleServices.GetOneMakaleUpdate(id,false);
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update([FromForm] MakaleDtoUpdate MakaleDto, IFormFile file)
        //{


        //    // Model doğrulama
        //    if (ModelState.IsValid)
        //    {
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
        //        using (var stream = new FileStream(path, FileMode.Create)) { await file.CopyToAsync(stream); }
        //        MakaleDto.MakaleImagesUrl = String.Concat("/images/", file.FileName);
        //        _manager.MakaleServices.OneUpdateMakale(MakaleDto);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult Delete([FromRoute(Name = "id")] int id)
        //{
        //    _manager.MakaleDataServices.DeleteMakaleDataByMakaleId(id);
        //    _manager.MakaleServices.Deletemakale(id);
        //    return RedirectToAction("Index");
        //}
    }

