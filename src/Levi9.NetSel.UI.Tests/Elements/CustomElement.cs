using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.UI.Tests.Elements
{
    public class CustomElement : NetSelElement
    {
        public CustomElement(NetSelElementProxy proxy) : base(proxy)
        {
        }

        public void CustomClick()
        {
            WebElement.Click();
        }

        public bool CustomIsPresent()
        {
            try
            {
                WebElement.GetAttribute("");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
