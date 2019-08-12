using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Contains methods for interaction with clickable elements.
    /// </summary>
    public class ClickableElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the ClickableElement class.
        /// </summary>
        public ClickableElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Clicks on element.
        /// </summary>
        public void Click()
        {
            WebElement.Click();
        }

        /// <summary>
        /// Checks if element is displayed.
        /// </summary>
        /// <returns>True if element is displayed, otherwise false.</returns>
        public bool IsDisplayed()
        {
            return WebElement.Displayed;
        }

        /// <summary>
        /// Checks if element is enabled.
        /// </summary>
        /// <returns>True if element is enabled, otherwise false.</returns>
        public bool IsEnabled()
        {
            return WebElement.Enabled;
        }
    }
}