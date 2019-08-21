using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Levi9.NetSel.Proxies
{
    public class NetSelElementProxy
    {
        private IWebElement _webElement;
        private readonly IWebDriver _webDriver;
        private readonly By _elementSelector;

        /// <summary>
        /// Initializes a new instance of the NetSelElementProxy class.
        /// </summary>
        /// <param name="webDriver">Instance of IWebDriver.</param>
        /// <param name="selector">Element selector.</param>
        public NetSelElementProxy(IWebDriver webDriver, By selector)
        {
            _webDriver = webDriver;
            _elementSelector = selector;
        }

        /// <summary>
        /// Initializes a new instance of the NetSelElementProxy class.
        /// </summary>
        /// <param name="webElement">Instance of IWebElement.</param>
        public NetSelElementProxy(IWebElement webElement)
        {
            _webElement = webElement;
            _webDriver = ((IWrapsDriver)webElement).WrappedDriver;
        }

        /// <summary>
        /// Returns instance of IWebElement.
        /// </summary>
        public IWebElement GetWebElement() => _webElement ?? (_webDriver.FindElement(_elementSelector));

        /// <summary>
        /// Returns list of IWebElements.
        /// </summary>
        public List<IWebElement> GetWebElements() => _webDriver.FindElements(_elementSelector).ToList();

        /// <summary>
        /// Returns instance of IWebDriver.
        /// </summary>
        public IWebDriver GetWebDriver() => _webDriver;
    }
}
