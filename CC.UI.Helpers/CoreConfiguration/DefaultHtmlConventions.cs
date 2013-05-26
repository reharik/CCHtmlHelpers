using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.TagConfiguration;

namespace CC.UI.Helpers.CoreConfiguration
{
    public class DefaultHtmlConventions : TagProfileExpression
    {
        public DefaultHtmlConventions()
        {
            Editors.IfPropertyIs<bool>().BuildBy(TagActionExpression.BuildCheckbox);
            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);
            Editors.Always.Modify(AddElementName);
            Displays.Always.BuildBy((req => new HtmlTag("span").Text(req.StringValue())));
            Labels.Always.BuildBy((req => new HtmlTag("span").Text(req.Accessor.Name)));
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (!tag.IsInputElement())
                return;
            tag.Attr("name", request.ElementId);
        }
    }
}