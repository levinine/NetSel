using Levi9.NetSel.Attributes;
using Levi9.NetSel.Handlers;
using Levi9.NetSel.UI.Tests.Elements;

namespace Levi9.NetSel.UI.Tests.PageFactory
{
    public class PageWithCustomElementExample
    {
        [Navigation(BaseUrl = "https://en.wikipedia.org")]
        public NavigationHandler Navigation { get; set; }

        [Window]
        public WindowHandler Window { get; set; }

        [Selector(Type = SelectorType.PartialLinkText, Value = "Create account")]
        public CustomElement CreateAccountLink { get; set; }

        [Selector(Type = SelectorType.PartialLinkText, Value = "Main Page")]
        public CustomElement MainPageLink { get; set; }
    }
}
