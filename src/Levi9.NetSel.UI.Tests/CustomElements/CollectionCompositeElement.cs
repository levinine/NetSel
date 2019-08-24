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
    public class CollectionCompositeElement : CompositeElement
    {
        public CollectionCompositeElement(NetSelElementProxy proxy) : base(proxy) { }

        [Selector(Type = SelectorType.TagName, Value = "h2")]
        public TextElement Headings { get; set; }
    }
}
