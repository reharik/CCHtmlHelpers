using System;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class DateDisplayBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && !def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("span").Attr("data-bind", "dateString:" + CCHtmlConventions2.DeriveElementName(request));
        }
    }

    public class TimeDisplayBuilder2 : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("span").Attr("data-bind", "timeString:" + CCHtmlConventions2.DeriveElementName(request));
        }
    }
}