using System;
using System.Configuration;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Handlers
{
    /// <summary>
    /// Contains support methods for handling navigation.
    /// </summary>
    public class NavigationHandler
    {
        private readonly IWebDriver _webDriver;

        private readonly string _baseUrl;
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the NavigationHandler class.
        /// </summary>
        /// <param name="proxy">Instance of NavigationHandlerProxy.</param>
        public NavigationHandler(NavigationHandlerProxy proxy)
        {
            _webDriver = proxy.WebDriver;
            _baseUrl = proxy.BaseUrl ?? ConfigurationManager.AppSettings["baseUrl"];
            _path = proxy.Path;
        }

        /// <summary>
        /// Method to navigate to page.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws in case where Base URL is not provided and cannot be found in configuration file.</exception>
        public void GoToPage()
        {
            _webDriver.Navigate().GoToUrl($"{_baseUrl}{_path ?? string.Empty}");
        }

        /// <summary>
        /// Refreshes current page.
        /// </summary>
        public void Refresh()
        {
            _webDriver.Navigate().Refresh();
        }
    }
}