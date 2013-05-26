using CC.UI.Helpers.HtmlTags;

namespace CC.UI.Helpers.InstanceConfiguration.Tags
{
    public class RadioButtonTag : HtmlTag

    {
            public RadioButtonTag(bool isChecked)
                : base("input")
            {
                Attr("type", "radio");
                if (isChecked)
                {
                    Attr("checked", "true");
                }
            }
        }
    }
