﻿using CC.UI.Helpers.ReflectionHelpers;

namespace CC.UI.Helpers.CoreConfiguration
{
    public class ElementRequest
    {
        private bool _hasFetched;
        private object _rawValue;

        public ElementRequest(object model, Accessor accessor)
        {
            Model = model;
            Accessor = accessor;
        }

        public string ElementId { get; set; }

        public object RawValue
        {
            get
            {
                if (!_hasFetched)
                {
                    _rawValue = Accessor.GetValue(Model);
                    _hasFetched = true;
                }
                return _rawValue;
            }
        }

        public object Model { get; private set; }

        public Accessor Accessor { get; private set; }

        public AccessorDef ToAccessorDef()
        {
            return new AccessorDef
                {
                    Accessor = Accessor,
                    ModelType = Model.GetType()
                };
        }

        public string StringValue()
        {
            if (RawValue == null || RawValue as string == string.Empty) return string.Empty;
            return RawValue.ToString();
            // I don't know what this is about. Why do we care if it's nullable or what it's type is?
            var type = RawValue.GetType();
            return type.IsNullable() ? type.GetInnerTypeFromNullable().ToString() : type.ToString();
        }
    }
}