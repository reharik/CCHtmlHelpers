using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class LinkDisplayBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(string)
                      && def.Accessor.HasAttribute<LinkDisplayAttribute>());
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("a");
            root.Attr("href", "#");
            root.Id(request.Accessor.FieldName);
            root.Append(new HtmlTag("span").Text(request.StringValue()));
            return root;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LinkDisplayAttribute : System.Attribute
    {
    }

}