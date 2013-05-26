namespace CC.UI.Helpers.HtmlTags
{
    public class HiddenTag : HtmlTag
    {
        public HiddenTag()
            : base("input")
        {
            Attr("type", "hidden");
        }
    }
}
