using System;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;

namespace CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries
{
    public static class CustomTagActionExpressions
    {
        public static HtmlTag BuildTextboxKO(ElementRequest request)
        {
            var date = DateTime.Parse(request.StringValue()).ToShortDateString();
            return new TextboxTag().Attr("value", date).AddClass("datePicker");
        }

    }
}
