using System;
using System.Linq.Expressions;

namespace CC.UI.Helpers.ReflectionHelpers
{
    public interface IValueGetter
    {
        string Name { get; }
        Type DeclaringType { get; }
        object GetValue(object target);

        Expression ChainExpression(Expression body);
    }
}