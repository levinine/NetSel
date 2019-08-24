using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Levi9.NetSel.Proxies
{
    public class NetSelElementProxy
    {
        private readonly By _elementSelector;
        private readonly Func<IWebElement> _locateElementFunction;
        private IWebDriver _webDriver;

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
        /// <param name="webDriver">Instance of IWebDriver.</param>
        /// <param name="locateElementFunction">Function required to locate an element.</param>
        public NetSelElementProxy(IWebDriver webDriver, Func<IWebElement> locateElementFunction)
        {
            _webDriver = webDriver;
            _locateElementFunction = locateElementFunction;
        }

        /// <summary>
        /// Initializes a new instance of the NetSelElementProxy class.
        /// </summary>
        /// <param name="webDriver">Instance of IWebDriver.</param>
        /// <param name="locateElementFunction">Function required to locate an element.</param>
        /// <param name="selector">Element selector.</param>
        public NetSelElementProxy(IWebDriver webDriver, Func<IWebElement> locateElementFunction, By selector)
        {
            _webDriver = webDriver;
            _locateElementFunction = locateElementFunction;
            _elementSelector = selector;
        }

        /// <summary>
        /// Returns instance of IWebElement.
        /// </summary>
        public IWebElement GetWebElement()
        {
            if (_locateElementFunction == null)
                return _webDriver.FindElement(_elementSelector);

            return _elementSelector == null ? 
                _locateElementFunction.Invoke() :
                _locateElementFunction.Invoke().FindElement(_elementSelector);
        }

        /// <summary>
        /// Returns list of IWebElements.
        /// </summary>
        public List<IWebElement> GetWebElements()
        {
            if (_locateElementFunction == null)
                return _webDriver.FindElements(_elementSelector).ToList();

            return _locateElementFunction.Invoke().FindElements(_elementSelector).ToList();
        }

        /// <summary>
        /// Returns instance of IWebDriver.
        /// </summary>
        public IWebDriver GetWebDriver() => _webDriver;

        public Func<IWebElement> GetLocateFunc() => _locateElementFunction;
    }
}