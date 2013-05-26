using System;
using System.Linq.Expressions;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.TagConfiguration
{
    public interface ITagGenerator<T> where T : class
    {
        string ElementPrefix { get; set; }

        T Model { get; set; }

        HtmlTag LabelFor(Expression<Func<T, object>> expression);

        HtmlTag InputFor(Expression<Func<T, object>> expression);

        HtmlTag DisplayFor(Expression<Func<T, object>> expression);

        ElementRequest GetRequest(Expression<Func<T, object>> expression);

        HtmlTag LabelFor(ElementRequest request);

        HtmlTag InputFor(ElementRequest request);

        HtmlTag DisplayFor(ElementRequest request);

        ElementRequest GetRequest<TProperty>(Expression<Func<T, TProperty>> expression);

        ElementRequest GetRequest(Accessor accessor);
    }
}