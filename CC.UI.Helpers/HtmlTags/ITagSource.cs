using System.Collections.Generic;

namespace CC.UI.Helpers.HtmlTags
{
    public interface ITagSource
    {
        IEnumerable<HtmlTag> AllTags();
    }
}