using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.HtmlTags;

namespace CC.UI.Helpers.InstanceConfiguration.Builders
{
    public class SelectFromIEnumerableBuilder : ElementBuilder
    {
        protected override bool matches(AccessorDef def)
        {
            var propertyName = def.Accessor.FieldName;
            var listPropertyInfo = def.ModelType.GetProperty(propertyName+"List");
            return listPropertyInfo != null && listPropertyInfo.PropertyType == typeof (IEnumerable<SelectListItem>);
        }

        public override HtmlTag Build(ElementRequest request)
        {
            Action<SelectTag> action = x =>
                                           {
//                                               var value = request.RawValue is Entity ? ((Entity)request.RawValue).EntityId : request.RawValue;
                                               var value = request.RawValue;
                                               
                                                var propertyName = request.ToAccessorDef().Accessor.FieldName;
                                                var listPropertyInfo = request.ToAccessorDef().ModelType.GetProperty(propertyName+"List");
                                               var selectListItems = listPropertyInfo.GetValue(request.Model, null) as IEnumerable<SelectListItem>;
                                               if (selectListItems == null) return;
                                               
                                               selectListItems.ForEachItem(option=> x.Option(option.Text, option.Value.IsNotEmpty() ? option.Value: ""));

                                               if ( value != null && value.ToString().IsNotEmpty())
                                               {
                                                   x.SelectByValue(value.ToString());
                                               }
                                               x.AddClass("fixedWidthDropdown");
                                           };
            return new SelectTag(action);
        }
    }
}