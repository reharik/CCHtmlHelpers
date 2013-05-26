using CC.UI.Helpers.HtmlTags;

namespace CC.UI.Helpers.InstanceConfiguration.Tags
{
    public class PasswordTag : HtmlTag
    {
        public PasswordTag() : base("input")
        {
            Attr("type", "password");
        }

        public PasswordTag(string name, string value)
            : this()
        {
            Attr("name", name);
            Attr("value", value);
        }

    }
}