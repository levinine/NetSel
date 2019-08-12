using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Elements
{
    /// <summary>
    /// Wrapper class that specifies methods for text elements.
    /// </summary>
    public class TextElement : NetSelElement
    {
        /// <summary>
        /// Initializes a new instance of the TextElement class.
        /// </summary>
        public TextElement(NetSelElementProxy proxy) : base(proxy) { }

        /// <summary>
        /// Returns element value attribute.
        /// </summary>
        /// <returns>Value of attribute "value".</returns>
        public string GetValue()
        {
            return GetAttribute("value");
        }

        /// <summary>
        /// Returns display text of an element. 
        /// </summary>
        /// <returns>Display text of an element.</returns>
        public string GetText()
        {
            return WebElement.Text;
        }
    }
}