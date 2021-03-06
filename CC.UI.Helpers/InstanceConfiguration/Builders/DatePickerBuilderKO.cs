using System;
using System.Collections.Generic;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries;
using CC.UI.Helpers.InstanceConfiguration.Tags;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class TextboxBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return true;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Attr("data-bind", "value:" + CCHtmlConventionsKO.DeriveElementName(request));
        }
    }

    public class DatePickerBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && !def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Attr("data-bind", "dateString:" + CCHtmlConventionsKO.DeriveElementName(request)).AddClass("datePicker");
        }
    }

    public class TimePickerBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return (def.Accessor.PropertyType == typeof(DateTime)
                || def.Accessor.PropertyType == typeof(DateTime?))
                && def.Accessor.FieldName.EndsWith("Time");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Attr("data-bind", "timeString:" + CCHtmlConventionsKO.DeriveElementName(request)).AddClass("timePicker");
        }
    }

    public class ImageBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(string)
                && def.Accessor.FieldName.EndsWith("FileUrl");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("img").Attr("data-bind",
                " attr: { src: " + CCHtmlConventionsKO.DeriveElementName(request) + " }")
                .Attr("alt", request.Accessor.FieldName);
        }
    }

    public class CheckboxBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof (bool);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new CheckboxTag(false).Attr("data-bind",
                                          "checked:" + CCHtmlConventionsKO.DeriveElementName(request));
        }
    }

    public class PasswordBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.Name.ToLowerInvariant().Contains("password");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new PasswordTag().Attr("data-bind", "value:" + CCHtmlConventionsKO.DeriveElementName(request));
        }
    }

    public class EmailDisplayBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.FieldName.ToLowerInvariant().Contains("email");
        }

        public override HtmlTag Build(ElementRequest request)
        {
            HtmlTag root = new HtmlTag("a");
            root.Attr("data-bind", "attr: { href: mailto:" + CCHtmlConventionsKO.DeriveElementName(request)+"}");
            root.Children.Add(new HtmlTag("span").Attr("data-bind", "text:" + CCHtmlConventionsKO.DeriveElementName(request)));
            return root;
        }
    }

    public class MultiSelectBuilderKO : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType.GetInterface("ITokenInputViewModel")!=null;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            return new TextboxTag().Id(request.Accessor.Name).AddClass("multiSelect").Attr("data-bind", "MultiSelect:" + CCHtmlConventionsKO.DeriveElementName(request));
        }
    }

//    public class PictureGallery : ElementBuilder
//    {
//        protected  override  bool matches(AccessorDef def)
//        {
//            return def.Accessor.PropertyType == typeof(IEnumerable<PhotoDto>);
//        }
//        public override HtmlTag Build(ElementRequest request)
//        {
//            var div = new HtmlTag("div").AddClass("gallery").Attr("data-bind", "foreach:" + CCHtmlConventions2.DeriveElementName(request));
//            var a = new HtmlTag("a").Attr("data-bind", "attr:{href:FileUrl_Thumb}");
//            a.Children.Add(new HtmlTag("image").Attr("data-bind", "attr:{src:FileUrl_Large,imageId:ImageId}"));
//            div.Children.Add(a);
//            return div;
//        }
//    }

    public class FileUploader : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            return def.Accessor.PropertyType == typeof(string)
               && (def.Accessor.FieldName.EndsWith("FileUrl"));
        }
        public override HtmlTag Build(ElementRequest request)
        {
            var container = new HtmlTag("div").AddClass("imageInputContainer");
            var imageContainer = new HtmlTag("div").AddClass("imageContainer");
            var name = CCHtmlConventionsKO.DeriveElementName(request);
            var thumb = new HtmlTag("img").Attr("data-bind", "attr: { src: " + name + " }").Id("image").Attr("alt", request.Accessor.FieldName);
            var linkImage = new HtmlTag("img").Attr("src",@"/content/images/document.png").Attr("alt", request.Accessor.FieldName);
            var link = new HtmlTag("a").Attr("data-bind", "attr: { href: " + name + "} ").Id("link").Attr("target", "_blank");
            link.Children.Add(linkImage);
            var delete = new HtmlTag("input").Attr("type", "button").AddClass("deleteImage").Attr("value", "Delete");

            var inputContainer = new HtmlTag("div").AddClass("inputContainer");
            var file = new HtmlTag("input").Attr("type", "file").Attr("size", 45).Attr("id", name).Attr("name", name);
            imageContainer.Children.Add(thumb);
            imageContainer.Children.Add(link);
            imageContainer.Children.Add(delete);
            inputContainer.Children.Add(file);
            container.Children.Add(imageContainer);
            container.Children.Add(inputContainer);
            return container;
        }
    }
}

