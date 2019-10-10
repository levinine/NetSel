using OpenQA.Selenium.Support.UI;

namespace Levi9.NetSel
{
    /// <summary>
    /// Wrapper class for Element and DriverWait.
    /// </summary>
    public class Wait<T>
    {
        /// <summary>
        /// Initializes a new instance of the Wait class.
        /// </summary>
        /// <param name="driverWait">Instance of WebDriverWait.</param>
        /// <param name="element">Collection of NetSel elements.</param>
        public Wait(WebDriverWait driverWait, T element)
        {
            DriverWait = driverWait;
            Element = element;
        }

        /// <summary>
        /// Property DriverWait.
        /// </summary>
        public WebDriverWait DriverWait { get; set; }

        /// <summary>
        /// Property Element.
        /// </summary>
        public T Element { get; set; }

        /// <summary>
        /// Property Reason.
        /// </summary>
        public string Reason { get; set; }
    }
}