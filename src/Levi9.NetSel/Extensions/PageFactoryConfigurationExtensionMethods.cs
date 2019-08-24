using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Levi9.NetSel.Attributes;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Internal;
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
            configuration.CreatePageAction = (resultClass, driver, parentProperty) =>
            {
                NetSelElementProxy GetElementProxy(PropertyInfo property)
                {
                    var selector = property.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy();

                    if (parentProperty == null)
                        return new NetSelElementProxy(driver, selector);

                    var locateFunction = resultClass.GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .First(x => x.Name == "LocateFunction").GetValue(resultClass);

                    var parentSelector = parentProperty.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy();

                    if (locateFunction != null)
                    {
                        return new NetSelElementProxy(driver,
                            () => ((Func<IWebElement>)locateFunction).Invoke().FindElement(parentSelector), selector);
                    }
                    return new NetSelElementProxy(driver, () => driver.FindElement(parentSelector), selector);
                }

                foreach (var propertyInfo in resultClass.GetType().GetProperties())
                {
                    if (propertyInfo.GetCustomAttribute<SelectorAttribute>() != null)
                    {
                        if (propertyInfo.PropertyType.IsCompositeType())
                        {
                            // Build composite elements
                            propertyInfo.SetValue(resultClass,
                                    configuration.ElementsBuilder?.BuildCompositeElement(propertyInfo.PropertyType,
                                        GetElementProxy(propertyInfo)));

                            // Build composite element properties
                            configuration.CreatePageAction(propertyInfo.GetValue(resultClass), driver, propertyInfo);
                            continue;
                        }

                        // Build non-composite elements
                        propertyInfo.SetValue(resultClass,
                            configuration.ElementsBuilder?.BuildElement(propertyInfo.PropertyType,
                                GetElementProxy(propertyInfo)));
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
                        propertyInfo.SetValue(resultClass,
                            configuration.HandlerBuilder?.BuildHandler(propertyInfo.PropertyType,
                                new WindowHandlerProxy(driver)));
                    }
                }
            };
            return configuration;
        }
    }
}