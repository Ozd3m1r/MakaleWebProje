using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.InterfaceClass;
using Entities.Models;
using MailChimp.Net.Models;
using MakaleWebProje.Extensions;


namespace MakaleWebProje.Pages
{
    public class MakaleKartlarıModel : PageModel
    {
        private readonly IServiceManager _manager;
        public Card Card { get; set; }
        public MakaleKartlarıModel(IServiceManager manager, Card CardServices)
        {
            _manager = manager;
            Card = CardServices;
        }
        public int CardId { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet()
        {
            ReturnUrl = ReturnUrl ?? "/";
            //Card = HttpContext.Session.GetJson<Cart>("Cart") ?? new Card();
        }
        public IActionResult OnPost(int MakaleId, string returnUrl)
        {
            Makale? makale = _manager.MakaleServices.GetOneMakale(MakaleId, true);
            if (makale != null)
            {
                // Cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();//models/SessionCart kurulu

                Card.AddItem(makale, 1);
                //HttpContext.Session.Setjson<Cart>("Cart", Cart);//models/SessionCart kurulu
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
      /*  public IActionResult OnPostRemove(int MakaleId, string returnUrl)
        {
            //   Cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();//models/SessionCart kurulu

            Cart.RemoveLine(Cart.Lines.First(c1 => c1.Product.ProductId.Equals(id)).Product);
            // HttpContext.Session.Setjson<Cart>("Cart", Cart);//models/SessionCart kurulu
            return Page();
        }*/
   }
}

