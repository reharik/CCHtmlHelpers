using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.InstanceConfiguration.HtmlExpressions;
using CC.UI.Helpers.ReflectionHelpers;
using CC.UI.Helpers.TagConfiguration;
using StructureMap;

namespace CC.UI.Helpers.InstanceConfiguration
{
    public static class CCUIHtmlExtensions
    {
        private static ITagGenerator<T> GetGenerator<T>(HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            TagGenerator<T> generator = ObjectFactory.Container.GetInstance<ITagGenerator<T>>() as TagGenerator<T>;
            generator.Model = helper.ViewData.Model;
            if (helper.ViewData.TemplateInfo.HtmlFieldPrefix.IsNotEmpty())
            {
                generator.ElementPrefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix + ".";
            }
            else
            {
                Accessor accessor = expression.ToAccessor();
                if (!accessor.OwnerType.Name.ToLowerInvariant().Contains("viewmodel"))
                {
                    generator.ElementPrefix = accessor.OwnerType.Name + ".";
                }
            }
            return generator;
        }

        public static HtmlTag InputCC<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator<T>(helper, expression);
            return generator.InputFor(expression);
        }
        
        public static HtmlTag LabelCC<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator<T>(helper, expression);
            HtmlTag tag = generator.LabelFor(expression);
            return tag;
        }

        public static HtmlTag DisplayCC<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator<T>(helper, expression);
            return generator.DisplayFor(expression);
        }

        public static EditorExpression<T> EditorInlineReverse<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new EditorExpression<T>(generator, expression).InlineReverse();
        }

        public static EditorExpression<T> SubmissionFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new EditorExpression<T>(generator, expression);
        }

        public static EditorExpression<T> DropdownSubmissionFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression, IEnumerable<SelectListItem> fillWith) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new EditorExpression<T>(generator, expression).FillWith(fillWith);
        }

        public static ViewExpression<T> ViewFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new ViewExpression<T>(generator, expression);
        }

        public static ViewDisplayExpression<T> ViewDisplayFor<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression) where T : class
        {
            ITagGenerator<T> generator = GetGenerator(helper, expression);
            return new ViewDisplayExpression<T>(generator, expression);
        }
    }
}