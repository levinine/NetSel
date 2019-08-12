using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Contains methods for interaction with check box elements.
    /// </summary>
    public class CheckBoxElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the CheckBoxElement class.
        /// </summary>
        public CheckBoxElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Checks the element.
        /// </summary>
        public void Check()
        {
            if (!IsChecked())
            {
                WebElement.Click();
            }
        }
        /// <summary>
        /// Checks if element is checked.
        /// </summary>
        /// <returns>True, if it is checked, otherwise false.</returns>
        public bool IsChecked()
        {
            return WebElement.Selected;
        }

        /// <summary>
        /// Unchecks the element.
        /// </summary>
        public void Uncheck()
        {
            if (IsChecked())
            {
                WebElement.Click();
            }
        }

        /// <summary>
        /// Returns element value attribute.
        /// </summary>
        /// <returns>Value of attribute "value".</returns>
        public string GetValue()
        {
            return GetAttribute("value");
        }

        /// <summary>
        /// Checks is element enabled.
        /// </summary>
        /// <returns>True if the element is enabled, otherwise false.</returns>
        public bool IsEnabled()
        {
            return WebElement.Enabled;
        }
    }
}