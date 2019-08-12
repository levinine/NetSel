using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Unit.Tests.TestData
{
    public class TestElement : NetSelElement
    {
        public TestElement(NetSelElementProxy proxy) : base(proxy) { }
    }
}