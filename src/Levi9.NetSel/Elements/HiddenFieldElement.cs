using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Contains methods for interaction with hidden field elements.
    /// </summary>
    public class HiddenFieldElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the HiddenFieldElement class.
        /// </summary>
        public HiddenFieldElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Sets the value for hidden field.
        /// </summary>
        /// <param name="value">Value to set.</param>
        public void SetFieldValue(string value)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript($"arguments[0].value='{value}';", WebElement);
        }
    }
}