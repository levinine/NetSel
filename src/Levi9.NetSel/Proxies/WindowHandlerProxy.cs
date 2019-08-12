using OpenQA.Selenium;

namespace Levi9.NetSel.Proxies
{
    public class WindowHandlerProxy : IHandlerProxy
    {
        public WindowHandlerProxy(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebDriver WebDriver { get; }
    }
}
