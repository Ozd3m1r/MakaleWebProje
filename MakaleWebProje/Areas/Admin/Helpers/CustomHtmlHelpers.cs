using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MakaleWebProje.Areas.Admin.Helpers
{

    public static class CustomHtmlHelpers
    {
        public static IHtmlContent StatisticsCard(this IHtmlHelper helper, string title, int count, string iconClass, string bgClass)
        {
            var card = new TagBuilder("div");
            card.AddCssClass($"card {bgClass} text-white shadow-sm");

            var cardBody = new TagBuilder("div");
            cardBody.AddCssClass("card-body");

            var heading = new TagBuilder("h5");
            var icon = new TagBuilder("i");
            icon.AddCssClass(iconClass);
            heading.InnerHtml.AppendHtml(icon);
            heading.InnerHtml.Append($" {title}");

            var paragraph = new TagBuilder("p");
            paragraph.InnerHtml.Append($"Toplam: ");
            var strong = new TagBuilder("strong");
            strong.InnerHtml.Append(count.ToString());
            paragraph.InnerHtml.AppendHtml(strong);

            cardBody.InnerHtml.AppendHtml(heading);
            cardBody.InnerHtml.AppendHtml(paragraph);
            card.InnerHtml.AppendHtml(cardBody);

            return card;
        }
    }
}
