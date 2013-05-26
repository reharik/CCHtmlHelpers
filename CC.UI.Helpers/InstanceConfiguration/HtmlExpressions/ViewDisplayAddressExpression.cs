using System;
using System.Linq.Expressions;
using CC.UI.Helpers.HtmlTags;
using CC.UI.Helpers.TagConfiguration;

namespace CC.UI.Helpers.InstanceConfiguration.HtmlExpressions
{
    public class ViewDisplayAddressExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private ITagGenerator<VIEWMODEL> _tagGenerator;
        private readonly Expression<Func<VIEWMODEL, object>> _address;

        public ViewDisplayAddressExpression(ITagGenerator<VIEWMODEL> tagGenerator, Expression<Func<VIEWMODEL, object>> address)
        {
            _tagGenerator = tagGenerator;
            _address = address;
        }

        public HtmlTag ToHtmlTag()
        {
            var displayFor = _tagGenerator.DisplayFor(_address);
            return displayFor;
        }
    }
}