using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Contains methods for interaction with input elements.
    /// </summary>
    public class InputElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the InputElement class.
        /// </summary>
        public InputElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Sends keys to input field.
        /// </summary>
        /// <param name="keys">Keys to send.</param>
        public void SendKeys(string keys)
        {
            WebElement.SendKeys(keys);
        }

        /// <summary>
        /// Clears input field content.
        /// </summary>
        public void ClearField()
        {
            WebElement.Clear();
        }

        /// <summary>
        /// Clears input field content and sends keys to it.
        /// </summary>
        /// <param name="keys">Keys to send.</param>
        public void ClearAndSendKeys(string keys)
        {
            ClearField();
            WebElement.SendKeys(keys);
        }
    }
}