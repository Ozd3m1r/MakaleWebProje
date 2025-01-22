using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MakaleWebProje.Areas.Admin.Helpers.TagHelpers
{
    public class StatisticsTagHelper : TagHelper
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public string Icon { get; set; }
        public string BackgroundColor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"card {BackgroundColor} text-white shadow-sm");

            var content = $@"
            <div class='card-body'>
                <h5><i class='{Icon}'></i> {Title}</h5>
                <p>Toplam: <strong>{Count}</strong></p>
            </div>";

            output.Content.SetHtmlContent(content);
        }
    }
}
