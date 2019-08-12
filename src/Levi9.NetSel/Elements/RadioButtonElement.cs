using OpenQA.Selenium;
using System.Collections.Generic;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Wrapper class that specifies methods for radio button elements.
    /// </summary>
    public class RadioButtonElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the RadioButtonElement class.
        /// </summary>
        public RadioButtonElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Selects the element.
        /// </summary>
        public void SelectValue()
        {
            WebElement.Click();
        }

        /// <summary>
        /// Gets element value attribute.
        /// </summary>
        /// <returns>Value of element.</returns>
        public string GetValue()
        {
           return GetAttribute("value");
        }

        /// <summary>
        /// Checks if element is selected.
        /// </summary>
        /// <returns>True if element is selected, otherwise false.</returns>
        public bool IsSelected()
        {
            return WebElement.Selected;
        }

        /// <summary>
        /// Selects the element by value attribute, after iterating through all elements.
        /// </summary>
        /// <param name="radioButtonList">List of radio button elements.</param>
        /// <param name="value">Value of element to select.</param>
        public void SelectByValue(List<IWebElement> radioButtonList, string value)
        {
            foreach (var rb in radioButtonList)
            {
                if (rb.GetAttribute("value").Equals(value))
                {
                    rb.Click();
                }
            }
        }
    }
}