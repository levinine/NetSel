using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Levi9.NetSel.Attributes;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Elements;
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
                foreach (var propertyInfo in resultClass.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (propertyInfo.GetCustomAttribute<SelectorAttribute>() != null)
                    {
                        bool IsElementComposite(PropertyInfo property)
                        {
                            return property.PropertyType.BaseType == typeof(CompositeElement);
                        }

                        bool IsCollectionOfComposite(PropertyInfo property)
                        {
                            if (property.PropertyType.GetGenericArguments().Any())
                            {
                                return property.PropertyType.GetGenericArguments()[0].BaseType == typeof(CompositeElement);
                            }

                            return false;
                        }

                        bool IsClassComposite(object resClass)
                        {
                            return resClass.GetType().BaseType == typeof(CompositeElement);
                        }

                        bool IsClassCollectionOfComposite(object resClass)
                        {
                            if (resClass.GetType().GetGenericArguments().Any())
                            {
                                return resultClass.GetType().GetGenericArguments()[0].BaseType == typeof(CompositeElement);
                            }

                            return false;
                        }

                        if (IsElementComposite(propertyInfo) || IsCollectionOfComposite(propertyInfo))
                        {
                            if (!IsClassComposite(resultClass) && !IsClassCollectionOfComposite(resultClass))
                            {
                                propertyInfo.SetValue(resultClass,
                                    configuration.ElementsBuilder?.BuildCompositeElement(propertyInfo.PropertyType,
                                        new NetSelElementProxy(driver,
                                            propertyInfo.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy())));
                            }
                            else
                            {
                                var locateFunction = resultClass.GetType()
                                    .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                    .First(x => x.Name == "LocateFunction").GetValue(resultClass);

                                Func<IWebElement> func;

                                var selector = propertyInfo.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy();
                                var parentSelector = parentProperty.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy();

                                if (locateFunction != null)
                                {
                                    func = () =>
                                        ((Func<IWebElement>)locateFunction).Invoke().FindElement(parentSelector);
                                }
                                else
                                {
                                    func = () => driver.FindElement(parentSelector);
                                }

                                propertyInfo.SetValue(resultClass,
                                    configuration.ElementsBuilder?.BuildCompositeElement(propertyInfo.PropertyType,
                                        new NetSelElementProxy(func, selector)));
                            }
                            configuration.CreatePageAction(propertyInfo.GetValue(resultClass), driver, propertyInfo);
                            continue;
                        }

                        if (parentProperty == null)
                        {
                            propertyInfo.SetValue(resultClass,
                                configuration.ElementsBuilder?.BuildElement(propertyInfo.PropertyType,
                                    new NetSelElementProxy(driver,
                                        propertyInfo.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy())));
                        }
                        else
                        {
                            var locateFunction = resultClass.GetType()
                                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                .First(x => x.Name == "LocateFunction").GetValue(resultClass);

                            Func<IWebElement> func;

                            var selector = propertyInfo.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy();
                            var parentSelector = parentProperty.GetCustomAttribute<SelectorAttribute>().MapToSeleniumBy();

                            if (locateFunction != null)
                            {
                                func = () =>
                                    ((Func<IWebElement>)locateFunction).Invoke().FindElement(parentSelector);
                            }
                            else
                            {
                                func = () => driver.FindElement(parentSelector);
                            }

                            propertyInfo.SetValue(resultClass,
                            configuration.ElementsBuilder?.BuildElement(propertyInfo.PropertyType,
                                new NetSelElementProxy(func, selector)));
                        }
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
                }
            };

            return configuration;
        }
    }
}