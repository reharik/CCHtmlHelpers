namespace CC.UI.Helpers.HtmlTags
{
    public class NoTag : HtmlTag
    {
        public NoTag() : base("")
        {
            Render(false);
        }
    }
}