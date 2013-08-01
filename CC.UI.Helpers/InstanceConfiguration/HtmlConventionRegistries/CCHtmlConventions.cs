using System;
using System.Linq;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.Builders;
using CC.UI.Helpers.InstanceConfiguration.Tags;
using CC.UI.Helpers.ReflectionHelpers;
using CC.UI.Helpers.TagConfiguration;

namespace CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries
{
    public class CCHtmlConventions : TagProfileExpression
    {
        public CCHtmlConventions()
        {
            numbers();
            EditorsChain();
            DisplaysChain();
            LabelsChain();
            validationAttributes();
        }

        public virtual void LabelsChain()
        {
            Labels.Always.BuildBy(
                req =>
                new HtmlTag("label").Attr("for", req.Accessor.Name).Text(req.Accessor.FieldName.ToSeperateWordsFromPascalCase()));
        }

        public virtual void DisplaysChain()
        {
            Displays.Builder<ImageBuilder>();
            Displays.Builder<EmailDisplayBuilder>();
            Displays.Builder<ListDisplayBuilder>();
            Displays.Builder<ImageFileDisplayBuilder>();
            Displays.Builder<DateFormatter>();
            Displays.Builder<TimeFormatter>();
            Displays.If(x => x.Accessor.PropertyType == typeof (DateTime) || x.Accessor.PropertyType == typeof (DateTime?))
                .BuildBy(req => req.RawValue != null
                        ? new HtmlTag("span").Text(DateTime.Parse(req.RawValue.ToString()).ToLongDateString())
                        : new HtmlTag("span"));
            Displays.Always.BuildBy(req => new HtmlTag("span").Text(req.StringValue()));
        }

        public virtual void EditorsChain()
        {
            Editors.Builder<GroupSelectedBuilder>();
            Editors.Builder<DatePickerBuilder>();
            Editors.Builder<TimePickerBuilder>();
            Editors.Builder<CheckboxBuilder>();
            Editors.If(x => x.Accessor.Name.ToLowerInvariant().Contains("password")).BuildBy(
                r => new PasswordTag().Attr("value", r.RawValue));
            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);
            Editors.Always.Modify(AddElementName);
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (tag.IsInputElement())
            {
                var name = request.Accessor.Name;
                if (request.Accessor is PropertyChain)
                {
                    name = ((PropertyChain)(request.Accessor)).PropertyNames.Aggregate((current, next) => current + "." + next);
                }
                tag.Attr("name", name);
            }
        }


        private void numbers()
        {
            Editors.IfPropertyIs<Int32>().Attr("max", Int32.MaxValue);
            Editors.IfPropertyIs<Int16>().Attr("max", Int16.MaxValue);
            //Editoin.IfPropertyIs<Int64>().Attr("max", Int64.MaxValue);
            Editors.IfPropertyTypeIs(IsIntegerBased).AddClass("integer");
            Editors.IfPropertyTypeIs(IsFloatingPoint).AddClass("number");
            Editors.IfPropertyTypeIs(IsIntegerBased).Attr("mask", "wholeNumber");
        }

        private void validationAttributes()
        {
//            Editors.Modifier<RequiredModifier>();
//            Editors.Modifier<PasswordConfirmModifier>();
//            Editors.Modifier<EmailModifier>();
//            Editors.Modifier<NumberModifier>();
//            Editors.Modifier<UrlModifier>();
//            Editors.Modifier<DateModifier>();
//            Editors.Modifier<RangeModifier>();
        }

        public static bool IsIntegerBased(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        public static bool IsFloatingPoint(Type type)
        {
            return type == typeof(decimal) || type == typeof(float) || type == typeof(double);
        }
    }
}