using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Levi9.NetSel.Attributes;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.UI.Tests.CustomElements
{
    public class CustomCompositeElement : CompositeElement
    {
        public CustomCompositeElement(NetSelElementProxy proxy) : base(proxy) { }

        [Selector(Type = SelectorType.Id, Value = "firstHeading")]
        public TextElement Title { get; set; }

        [Selector(Type = SelectorType.Id, Value = "mp-topbanner")]
        public NestedCompositeElement Banner { get; set; }

        [Selector(Type = SelectorType.TagName, Value = "td")]
        public ElementCollection<CollectionCompositeElement> TableColumns { get; set; }
    }
}
