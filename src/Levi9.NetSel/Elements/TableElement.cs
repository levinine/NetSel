using OpenQA.Selenium;
using System.Collections.Generic;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Wrapper class that specifies methods for table elements.
    /// </summary>
    public class TableElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the TableElement class.
        /// </summary>
        public TableElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Returns cell by its content.
        /// </summary>
        /// <param name="content">Cell content.</param>
        /// <returns>IWebElement that represents searched cell.</returns>
        public IWebElement GetCellByContent(string content)
        {
            return WebElement.FindElement(By.XPath($"//td[contains(.,'{content}')]"));
        }
       
        /// <summary>
        /// Returns the text of cell by its selector.
        /// </summary>
        /// <param name="selector">Element selector.</param>
        /// <returns>Text of cell.</returns>
        public string GetCellBySelector(By selector)
        {
            return WebElement.FindElement(selector).Text;
        }
        
        /// <summary>
        /// Returns all row elements.
        /// </summary>
        /// <returns>List of row elements.</returns>
        public IList<IWebElement> GetRowElements()
        {
            return WebElement.FindElements(By.TagName("tr"));
        }
        
        /// <summary>
        /// Returns all column elements.
        /// </summary>
        /// <returns>List of all column elements.</returns>
        public IList<IWebElement> GetHeaderElements()
        {
            return WebElement.FindElements(By.TagName("th"));
        }
        
        /// <summary>
        /// Returns number of rows in table.
        /// </summary>
        /// <returns>Number of rows in table.</returns>
        public int GetRowSize()
        {
            return WebElement.FindElements(By.TagName("tr")).Count;
        }

        /// <summary>
        /// Returns number of columns in table.
        /// </summary>
        /// <returns>Number of columns in table.</returns>
        public int GetColumnSize()
        {
            return WebElement.FindElements(By.TagName("th")).Count;
        }
    }
}