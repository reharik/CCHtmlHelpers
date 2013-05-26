using System;
using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.CoreConfiguration
{
    public interface IElementNamingConvention
    {
        string GetName(Type modelType, Accessor accessor);
    }
}