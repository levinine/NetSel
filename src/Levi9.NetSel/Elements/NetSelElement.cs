using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Wrapper class for IWebElement
    /// </summary>
    public abstract class NetSelElement
    {
        private readonly NetSelElementProxy _proxy;

        /// <summary>
        /// Initializes a new instance of the NetSelElement class.
        /// </summary>
        /// <param name="proxy">Instance of NetSelElementProxy class.</param>
        protected NetSelElement(NetSelElementProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Returns value of an attribute.
        /// </summary>
        /// <param name="attribute">Attribute name.</param>
        /// <returns>Attribute value.</returns>
        public string GetAttribute(string attribute)
        {
            return WebElement.GetAttribute(attribute);
        }

        /// <summary>
        /// Returns new instance of IWebElement.
        /// </summary>
        public virtual IWebElement WebElement => _proxy.GetWebElement();

        /// <summary>
        /// Returns WebDriver.
        /// </summary>
        public virtual IWebDriver Driver => _proxy.GetWebDriver();

        /// <summary>
        /// Returns WebElement tag name.
        /// </summary>
        public string TagName => WebElement.TagName;
        /// <summary>
        /// Returns whether is element displayed.
        /// </summary>
        public bool Displayed => WebElement.Displayed;

        /// <summary>
        /// Returns whether is element enabled.
        /// </summary>
        public bool Enabled => WebElement.Enabled;

        /// <summary>
        /// Returns text of an element.
        /// </summary>
        public string Text => WebElement.Text;
    }
}