using System.Collections.Generic;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Wrapper class for list of NetSel elements.
    /// </summary>
    public class ElementCollection<T>
    {
        private readonly NetSelElementProxy _proxy;

        /// <summary>
        /// Initializes a new instance of the ElementCollection class.
        /// </summary>
        /// <param name="proxy">Instance of NetSelElementProxy class.</param>
        public ElementCollection(NetSelElementProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Returns array of IWebElements.
        /// </summary>
        /// <returns>Array of IWebElements.</returns>
        public T[] GetElements()
        {
            var elementList = new List<T>();
            _proxy.GetWebElements().ForEach(element => elementList
                .Add((T)PageFactory.GetConfiguration().ElementsBuilder
                    .BuildElement(typeof(T), new NetSelElementProxy(() => element))));
            return elementList.ToArray();
        }

        /// <summary>
        /// Returns IWebDriver.
        /// </summary>
        public IWebDriver WebDriver => _proxy.GetWebDriver();
    }
}