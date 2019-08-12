using Levi9.NetSel.Elements;
using Levi9.NetSel.WaitConditions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Levi9.NetSel
{
    /// <summary>
    /// Contains WaitCondition methods that are used by WaitExtensionMethods.
    /// </summary>
    public class Until
    {
        /// <summary>
        /// Checks if element is present in DOM.
        /// </summary>
        /// <param name="element">Instance of NetSelElement.</param>
        /// <returns>True if present, otherwise false.</returns>
        private static bool IsElementPresent(NetSelElement element)
        {
            try
            {
                return element.TagName != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Condition to wait until element exists.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> Exists => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.TagName != null));

        /// <summary>
        /// Condition to wait until element does not exist.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> NotExists => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && !elements.All(IsElementPresent));
        
        /// <summary>
        /// Condition to wait until element is visible.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> Visible => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed));
        
        /// <summary>
        /// Condition to wait until element is not visible.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> NotVisible => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => !element.Displayed));
        
        /// <summary>
        /// Condition to wait until element is clickable.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> Clickable => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed && element.Enabled));

        /// <summary>
        /// Condition to wait until element is not clickable.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> NotClickable => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed && !element.Enabled));
        
        /// <summary>
        /// Condition to wait until select element option is selected using text.
        /// </summary>
        /// <param name="text">Text from option that is expected to be selected.</param>
        /// <returns>Instance of WaitCondition.</returns>
        public static WaitCondition<ICollection<NetSelElement>> OptionIsSelectedUsingText(string text) => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed && ((SelectWebElement)element).GetSelectedOption().Text == text));
        
        /// <summary>
        /// Condition to wait until select element option is not selected using text.
        /// </summary>
        /// <param name="text">Text from option that is not expected to be selected.</param>
        /// <returns>Instance of WaitCondition.</returns>
        public static WaitCondition<ICollection<NetSelElement>> OptionIsNotSelectedUsingText(string text) => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed && ((SelectWebElement)element).GetSelectedOption().Text != text));
        
        /// <summary>
        /// Condition to wait until select element option is selected using value.
        /// </summary>
        /// <param name="value">Value from option that is expected to be selected.</param>
        /// <returns>Instance of WaitCondition.</returns>
        public static WaitCondition<ICollection<NetSelElement>> OptionIsSelectedUsingValue(string value) => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed && ((SelectWebElement)element).GetSelectedOption().GetAttribute("value") == value));
       
        /// <summary>
        /// Condition to wait until select element option is selected using value.
        /// </summary>
        /// <param name="value">Value from option that is not expected to be selected.</param>
        /// <returns>Instance of WaitCondition.</returns>
        public static WaitCondition<ICollection<NetSelElement>> OptionIsNotSelectedUsingValue(string value) => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Displayed && ((SelectWebElement)element).GetSelectedOption().GetAttribute("value") != value));
        
        /// <summary>
        /// Condition to wait until element is selected.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> Selected => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => ((CheckBoxElement)element).IsChecked()));
        
        /// <summary>
        /// Condition to wait until element is not selected.
        /// </summary>
        public static WaitCondition<ICollection<NetSelElement>> NotSelected => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => !((CheckBoxElement)element).IsChecked()));
       
        /// <summary>
        /// Condition to wait until specified text is present in element.
        /// </summary>
        /// <param name="text">Text expected to be present.</param>
        /// <returns>Instance of WaitCondition.</returns>
        public static WaitCondition<ICollection<NetSelElement>> TextToBePresentInElement(string text) => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Text == text));
       
        /// <summary>
        /// Condition to wait until specified text is not present in element.
        /// </summary>
        /// <param name="text">Text expected not to be present.</param>
        /// <returns>Instance of WaitCondition.</returns>
        public static WaitCondition<ICollection<NetSelElement>> TextNotToBePresentInElement(string text) => new WaitCondition<ICollection<NetSelElement>>(elements => elements.Count != 0 && elements.All(element => element.Text != text));
    }
}