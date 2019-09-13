using Levi9.NetSel.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Levi9.NetSel.Extensions
{
    /// <summary>
    /// Contains extension methods for NetSel elements.
    /// </summary>
    public static class FocusHandler
    {
        /// <summary>
        /// Method to focus web element.
        /// </summary>
        /// <param name="webElement">Instance of NetSelElement.</param>
        public static void FocusElement<T>(this T webElement) where T : NetSelElement
        {
            var action = new Actions(webElement.Driver)
                .MoveToElement(webElement.WebElement);

            if (typeof(T) == typeof(InputElement))
                action.Click();

            action.Perform();
        }
    }
}