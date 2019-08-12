using System.Reflection;
using Levi9.NetSel.Attributes;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Extensions
{
    /// <summary>
    /// Contains extension methods for configuring page factory.
    /// </summary>
    public static class PageFactoryConfigurationExtensionMethods
    {
        /// <summary>
        /// Configures NetSel element builder.
        /// </summary>
        /// <param name="configuration">Instance of PageFactoryConfiguration.</param>
        /// <returns>Page factory configuration.</returns>
        public static PageFactoryConfiguration ConfigureNetSelElementsBuilder(this PageFactoryConfiguration configuration)
        {
            var elementsBuilder = new ElementsBuilder();
            elementsBuilder.RegisterNetSelTypes();
            configuration.ElementsBuilder = elementsBuilder;
            return configuration;
        }

        /// <summary>
        /// Configures NetSel handler builder.
        /// </summary>
        /// <param name="configuration">Instance of PageFactoryConfiguration.</param>
        /// <returns>Page factory configuration.</returns>
        public static PageFactoryConfiguration ConfigureNetSelHandlerBuilder(this PageFactoryConfiguration configuration)
        {
            var handlerBuilder = new HandlerBuilder();
            handlerBuilder.RegisterNetSelHandlerTypes();
            configuration.HandlerBuilder = handlerBuilder;
            return configuration;
        }

        public static PageFactoryConfiguration ConfigurePageCreation(this PageFactoryConfiguration configuration)
        {
            configuration.CreatePageAction = (resultClass, driver) =>
            {
                foreach (var propertyInfo in resultClass.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (propertyInfo.GetCustomAttribute<SelectorAttribute>() != null)
                    {
                        propertyInfo.SetValue(resultClass,
                            configuration.ElementsBuilder?.BuildElement(propertyInfo.PropertyType,
                                new NetSelElementProxy(driver, propertyInfo.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy())));
                    }
                    else if (propertyInfo.GetCustomAttribute<NavigationAttribute>() != null)
                    {
                        var attr = propertyInfo.GetCustomAttribute<NavigationAttribute>();
                        propertyInfo.SetValue(resultClass,
                            configuration.HandlerBuilder?.BuildHandler(propertyInfo.PropertyType,
                                new NavigationHandlerProxy(driver, attr.BaseUrl, attr.Path)));
                    }
                    else if (propertyInfo.GetCustomAttribute<WindowAttribute>() != null)
                    {
                        var attr = propertyInfo.GetCustomAttribute<WindowAttribute>();
                        propertyInfo.SetValue(resultClass,
                            configuration.HandlerBuilder?.BuildHandler(propertyInfo.PropertyType,
                                new WindowHandlerProxy(driver)));
                    }
                    else if (propertyInfo.PropertyType == typeof(IWebDriver))
                    {
                        propertyInfo.SetValue(resultClass, driver);
                    }
                }
            };

            return configuration;
        }
    }
}