using Levi9.NetSel.Attributes;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.UI.Tests.CustomElements
{
    public class NestedCompositeElement : CompositeElement
    {
        public NestedCompositeElement(NetSelElementProxy proxy) : base(proxy) { }

        [Selector(Type = SelectorType.TagName, Value = "a")]
        public ElementCollection<TextElement> Links { get; set; }

        [Selector(Type = SelectorType.TagName, Value = "ul")]
        public Nested2CompositeElement UnorderedList { get; set; }
    }
}
