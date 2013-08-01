using System;
using System.Linq;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.Builders;
using CC.UI.Helpers.ReflectionHelpers;
using CC.UI.Helpers.TagConfiguration;

namespace CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries
{
    public class CCHtmlConventionsKO : TagProfileExpression
    {
        public CCHtmlConventionsKO()
        {
            EditorsChain();
            DisplaysChain();
            LabelsChain();
            
            numbers();
            validationAttributes();

        }

        public virtual void LabelsChain()
        {
           Labels.Always.BuildBy(req =>
                                      {
                                          var htmlTag = new HtmlTag("label").Attr("for", req.Accessor.Name);
                                          var display = req.Accessor.FieldName;
                                          htmlTag.Text(display.ToSeperateWordsFromPascalCase());
                                          return htmlTag;
                                      });
        }

        public virtual void DisplaysChain()
        {
            Displays.Builder<ImageBuilderKO>();
            Displays.Builder<EmailDisplayBuilderKO>();
            Displays.Builder<ListDisplayBuilder>();
            Displays.Builder<DateDisplayBuilderKO>();
            Displays.Builder<TimeDisplayBuilderKO>();
            Displays.Builder<ImageFileDisplayBuilder>();
            Displays.Always.BuildBy(req => new HtmlTag("span").Attr("data-bind", "text:" + DeriveElementName(req)));
        }

        public virtual void EditorsChain()
        {
            Editors.Builder<SelectFromIEnumerableBuilderKO>();
            Editors.Builder<DatePickerBuilderKO>();
            Editors.Builder<TimePickerBuilderKO>();
            Editors.Builder<CheckboxBuilderKO>();
            Editors.Builder<PasswordBuilderKO>();
            Editors.Builder<MultiSelectBuilderKO>();
//          Editors.Builder.Builder<PictureGallery>();
            Editors.Builder<FileUploader>();
            // default builder
            Editors.Builder<TextboxBuilderKO>();
            Editors.Always.Modify(AddElementName);
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (tag.IsInputElement())
            {
                tag.Attr("name", DeriveElementName(request));
            }
        }

        public static string DeriveElementName(ElementRequest request)
        {
            var name = request.Accessor.Name;
            if (request.Accessor is PropertyChain)
            {
                name = ((PropertyChain)(request.Accessor)).PropertyNames.Aggregate((current, next) => current + "." + next);
                var isDomainEntity = false;
                var de = request.Accessor.PropertyType.BaseType;
                while (de.Name != "Object")
                {
                    if (de.Name == "DomainEntity") isDomainEntity = true;
                    de = de.BaseType;
                }
                if (isDomainEntity) name += ".EntityId";

            }
            return name;
        }

        private void numbers()
        {
            Editors.IfPropertyIs<Int32>().Modify(x=>{if(x.TagName()==new TextboxTag().TagName()) x.Attr("max", Int32.MaxValue);});
            Editors.IfPropertyIs<Int16>().Modify(x => { if (x.TagName() == new TextboxTag().TagName()) x.Attr("max", Int16.MaxValue); });
            Editors.IfPropertyIs<Int64>().Modify(x => { if (x.TagName() == new TextboxTag().TagName()) x.Attr("max", Int64.MaxValue); });
            Editors.IfPropertyTypeIs(IsIntegerBased).Modify(x=>{if(x.TagName()==new TextboxTag().TagName()) x.AddClass("integer");});
            Editors.IfPropertyTypeIs(IsFloatingPoint).Modify(x=>{if(x.TagName()==new TextboxTag().TagName()) x.AddClass("number");});
            Editors.IfPropertyTypeIs(IsIntegerBased).Modify(x => { if (x.TagName() == new TextboxTag().TagName()) x.Attr("mask", "wholeNumber"); });
        }

        private void validationAttributes()
        {
//            Editors.Modifier<RequiredModifier>();
//            Editors.Modifier<NumberModifier>();
//            Editors.Modifier<FileRequiredModifier>();
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