using System;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries
{
    public class CCElementNamingConvention : IElementNamingConvention
    {
        public string GetName(Type modelType, Accessor accessor)
        {
            return accessor.FieldName;
        }
    }
}