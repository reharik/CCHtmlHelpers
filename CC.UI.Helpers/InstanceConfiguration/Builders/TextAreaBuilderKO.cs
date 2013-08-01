using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries;
using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class TextAreaBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(string)
                    && def.Accessor.HasAttribute<TextAreaAttribute>());
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("textarea").Attr("data-bind", "value:" + CCHtmlConventionsKO.DeriveElementName(request)).AddClass("textArea").Attr("name", request.ElementId);
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class TextAreaAttribute : System.Attribute
    {
    }
}

