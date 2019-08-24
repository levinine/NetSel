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
    public class Nested2CompositeElement : CompositeElement
    {
        public Nested2CompositeElement(NetSelElementProxy proxy) : base(proxy) { }

        [Selector(Type = SelectorType.TagName, Value = "a")]
        public ElementCollection<TextElement> Links { get; set; }
    }
}
