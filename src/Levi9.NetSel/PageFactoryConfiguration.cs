using System;
using Levi9.NetSel.Builders;
using OpenQA.Selenium;

namespace Levi9.NetSel
{
    /// <summary>
    /// PageFactory configuration.
    /// </summary>
    public class PageFactoryConfiguration
    {
        /// <summary>
        /// Property ElementsBuilder.
        /// </summary>
        public IElementsBuilder ElementsBuilder { get; set; }

        /// <summary>
        /// Property HandlerBuilder.
        /// </summary>
        public IHandlerBuilder HandlerBuilder { get; set; }

        /// <summary>
        /// Property CreatePageAction.
        /// </summary>
        public Action<object, IWebDriver> CreatePageAction { get; set; }
    }
}