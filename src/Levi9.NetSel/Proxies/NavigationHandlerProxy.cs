using OpenQA.Selenium;

namespace Levi9.NetSel.Proxies
{
    public class NavigationHandlerProxy : IHandlerProxy
    {
        public NavigationHandlerProxy(IWebDriver webDriver, string baseUrl, string path)
        {
            WebDriver = webDriver;
            BaseUrl = baseUrl;
            Path = path;
        }

        public IWebDriver WebDriver { get; }

        public string BaseUrl { get; }

        public string Path { get; }
    }
}
