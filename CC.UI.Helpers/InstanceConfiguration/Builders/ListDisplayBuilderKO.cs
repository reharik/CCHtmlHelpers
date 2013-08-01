using System.Collections.Generic;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class ListDisplayBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (IEnumerable<string>);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("div").Attr("data-bind", "foreach: "+ CCHtmlConventionsKO.DeriveElementName(request));
            var child = new HtmlTag("div").Attr("data-bind", "text: $data" );
            root.Append(child);
            return root;
        }
    }
}