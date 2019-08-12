using System;
using Levi9.NetSel.Attributes;
using OpenQA.Selenium;

namespace Levi9.NetSel.Extensions
{
    /// <summary>
    /// Contains extension methods for selectors.
    /// </summary>
    public static class SelectorExtensionMethods
    {
        /// <summary>
        /// Maps SelectorType to By.
        /// </summary>
        /// <param name="selector">Instance of SelectorAttribute.</param>
        /// <returns>By selector.</returns>
        public static By MapToSeleniumBy(this SelectorAttribute selector)
        {
            switch(selector.Type)
            {
                case SelectorType.Id:
                    return By.Id(selector.Value);

                case SelectorType.Name:
                    return By.Name(selector.Value);

                case SelectorType.TagName:
                    return By.TagName(selector.Value);

                case SelectorType.ClassName:
                    return By.ClassName(selector.Value);

                case SelectorType.CssSelector:
                    return By.CssSelector(selector.Value);

                case SelectorType.LinkText:
                    return By.LinkText(selector.Value);

                case SelectorType.PartialLinkText:
                    return By.PartialLinkText(selector.Value);

                case SelectorType.XPath:
                    return By.XPath(selector.Value);

                default:
                    throw new NotSupportedException($"Mapping for selector type: ${selector.Type} is not supported.");
            }
        }
    }
}