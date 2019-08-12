using Levi9.NetSel.Attributes;
using Levi9.NetSel.Handlers;

namespace Levi9.NetSel.Unit.Tests.TestData
{
    public class TestPage
    {
        [Selector(Type = SelectorType.ClassName, Value = "test")]
        public TestElement TestElement { get; set; }

        [Navigation(Path = "/demo")]
        public NavigationHandler TestNavigation { get; set; }
    }
}