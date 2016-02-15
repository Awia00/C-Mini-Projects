using Microsoft.AspNet.Razor.TagHelpers;

namespace PortifolioSite
{
    /// <summary>
    /// To use this I also added it to _ViewImports
    /// </summary>
    [HtmlTargetElement(Attributes = "tag-helper-test")]
    public class TagHelperTest : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetContent("A tag helper item");
        }
    }
}
