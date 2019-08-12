using Levi9.NetSel.Elements;
using OpenQA.Selenium;

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
        public static void FocusElement(this NetSelElement webElement)
        {
            ((IJavaScriptExecutor)webElement.Driver).ExecuteScript("arguments[0].focus();", webElement);
        }
    }
}