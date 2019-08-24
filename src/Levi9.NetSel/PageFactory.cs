using System;
using System.Collections.Generic;
using System.Reflection;
using Levi9.NetSel.Attributes;
using Levi9.NetSel.Extensions;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel
{
    /// <summary>
    /// Contains PageFactory implementation.
    /// </summary>
    public static class PageFactory
    {
        private static PageFactoryConfiguration _configuration;
        static PageFactory()
        {
            Configure(configuration => new PageFactoryConfiguration()
                .ConfigureNetSelElementsBuilder()
                .ConfigureNetSelHandlerBuilder()
                .ConfigurePageCreation());
        }

        /// <summary>
        /// Returns configuration.
        /// </summary>
        /// <returns>PageFactory configuration.</returns>
        public static PageFactoryConfiguration GetConfiguration()
        {
            return _configuration;
        }

        /// <summary>
        /// Invokes configuration handler.
        /// </summary>
        /// <param name="configurationHandler">Function for handling configuration.</param>
        public static void Configure(Func<PageFactoryConfiguration, PageFactoryConfiguration> configurationHandler)
        {
            _configuration = configurationHandler.Invoke(new PageFactoryConfiguration());
        }

        /// <summary>
        /// Creates instance of page.
        /// </summary>
        /// <typeparam name="T">Type of page.</typeparam>
        /// <param name="driver">Instance of IWebDriver.</param>
        /// <returns>Object.</returns>
        public static T CreatePage<T>(IWebDriver driver) where T : class, new()
        {
            var resultClass = new T();

            _configuration.CreatePageAction.Invoke(resultClass, driver, null);

            return resultClass;
        }
    }
}