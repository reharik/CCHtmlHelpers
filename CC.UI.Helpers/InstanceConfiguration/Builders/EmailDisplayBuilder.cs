using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class EmailDisplayBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.FieldName.ToLowerInvariant().Contains("email");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("a");
            root.Attr("href", "mailto:" + request.StringValue());
            root.Attr("name", request.Accessor.FieldName + "Link");
            root.Append(new HtmlTag("span").Text(request.StringValue()));
            return root;
        }
    }

}