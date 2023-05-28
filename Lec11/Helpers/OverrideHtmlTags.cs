using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lec11.Helpers
{
    [HtmlTargetElement("big")]
    [HtmlTargetElement(Attributes = "big")]
    public class OverrideHtmlTags : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h3";
            output.Attributes.RemoveAll("big");
            output.Attributes.SetAttribute("class", "text-danger");

        }
    }
}
