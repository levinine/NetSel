using OpenQA.Selenium;
using System.Collections.Generic;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium.Support.UI;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Wrapper class that specifies methods for select elements.
    /// </summary>
    public class SelectWebElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the SelectWebElement class.
        /// </summary>
        public SelectWebElement(NetSelElementProxy proxy) : base(proxy) { }

        private IEnumerable<IWebElement> Options => WebElement.FindElements(By.TagName("option"));

        /// <summary>
        /// Selects value in dropdown using text.
        /// </summary>
        /// <param name="text">Text of option to select.</param>
        public void SelectByText(string text)
        {
            new SelectElement(WebElement).SelectByText(text);
        }

        /// <summary>
        /// Selects value in dropdown using value attribute.
        /// </summary>
        /// <param name="value">Value of option to select.</param>
        public void SelectByValue(string value)
        {
            new SelectElement(WebElement).SelectByValue(value);
        }

        /// <summary>
        /// Selects multiple values using text.
        /// </summary>
        /// <param name="values">List of string values to be selected.</param>
        public void SelectMultipleValuesUsingText(List<string> values)
        {
            var element = new SelectElement(WebElement);

            foreach (var value in values)
            {
                element.SelectByText(value);
            }
        }

        /// <summary>
        /// Deselects element using text.
        /// </summary>
        /// <param name="text">Text of element to be deselected.</param>
        public void DeselectByText(string text)
        {
            new SelectElement(WebElement).DeselectByText(text);
        }

        /// <summary>
        /// Deselects all values.
        /// </summary>
        public void DeselectAll()
        {
            new SelectElement(WebElement).DeselectAll();
        }

        /// <summary>
        /// Returns currently selected option.
        /// </summary>
        /// <returns>IWebElement that represents currently selected option.</returns>
        public IWebElement GetSelectedOption()
        {
            foreach (var option in Options)
            {
                if (option.Selected)
                    return option;
            }
            throw new NoSuchElementException("No option is selected.");
        }
    }
}