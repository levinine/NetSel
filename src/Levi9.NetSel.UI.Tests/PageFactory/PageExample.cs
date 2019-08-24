using Levi9.NetSel.Attributes;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Handlers;
using Levi9.NetSel.UI.Tests.CustomElements;

namespace Levi9.NetSel.UI.Tests.PageFactory
{
    public class PageExample
    {
        [Navigation(BaseUrl = "https://en.wikipedia.org/", Path = "wiki/Main_Page")]
        public NavigationHandler Navigation { get; set; }

        [Selector(Type = SelectorType.Id, Value = "content")]
        public CustomCompositeElement BodyContent { get; set; }
    }
}