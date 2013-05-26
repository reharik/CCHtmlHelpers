using System;
using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.CoreConfiguration
{
    public class DefaultElementNamingConvention : IElementNamingConvention
    {
        #region IElementNamingConvention Members

        public string GetName(Type modelType, Accessor accessor)
        {
            return accessor.Name;
        }

        #endregion
    }
}