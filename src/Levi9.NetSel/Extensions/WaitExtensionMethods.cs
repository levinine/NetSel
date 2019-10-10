using System;
using System.Collections.Generic;
using System.Linq;
using Levi9.NetSel.Elements;
using Levi9.NetSel.WaitConditions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Levi9.NetSel.Extensions
{
    /// <summary>
    /// Contains extension methods for handling wait events.
    /// </summary>
    public static class WaitExtensionMethods
    {
        private static bool EvaluateWaitConditions<T>(this IEnumerable<IWaitCondition<T>> conditions, T elements)
        {
            return conditions.All(x => x.GetCondition().Invoke(elements));
        }

        private static bool EvaluateWaitCondition<T>(this IWaitCondition<T> condition, T elements)
        {
            return condition.GetCondition().Invoke(elements);
        }

        private static void Wait<T>(Wait<T> wait, Func<IWebDriver, bool> waitCondition)
        {
            try
            {
                wait.DriverWait.Until(waitCondition);
            }
            catch (WebDriverTimeoutException e)
            {
                if (!string.IsNullOrEmpty(wait.Reason))
                    throw new WebDriverTimeoutException(wait.Reason, e);

                throw e;
            }
        }

        /// <summary>
        /// Method to create new instance of Wait.
        /// </summary>
        /// <param name="elem">Instance of NetSelElement.</param>
        /// <param name="timeoutTime">Time interval.</param>
        /// <returns>Instance of Wait.</returns>
        public static Wait<ICollection<NetSelElement>> WaitFor(this NetSelElement elem, TimeSpan timeoutTime)
        {
            return WaitFor(new[] { elem }, timeoutTime);
        }

        /// <summary>
        /// Method to create new instance of Wait.
        /// </summary>
        /// <param name="elem">Array of NetSel elements.</param>
        /// <param name="timeoutTime">Time interval.</param>
        /// <returns>Instance of Wait.</returns>
        public static Wait<ICollection<NetSelElement>> WaitFor(this ICollection<NetSelElement> elem, TimeSpan timeoutTime)
        {
            if (elem.Count == 0)
            {
                throw new ArgumentException("Element array cannot be empty.");
            }
            return new Wait<ICollection<NetSelElement>>(new WebDriverWait(elem.First().Driver, timeoutTime), elem);
        }

        /// <summary>
        /// Method to create new instance of Wait.
        /// </summary>
        /// <param name="elem">Element collection of NetSel elements.</param>
        /// <param name="timeoutTime">Time interval.</param>
        /// <returns>Instance of Wait.</returns>
        public static Wait<ElementCollection<T>> WaitFor<T>(this ElementCollection<T> elem, TimeSpan timeoutTime)
        {
            return new Wait<ElementCollection<T>>(new WebDriverWait(elem.WebDriver, timeoutTime), elem);
        }

        /// <summary>
        /// Method to add custom reason for exception message.
        /// </summary>
        /// <param name="wait">Instance of Wait.</param>
        /// <param name="message">Exception message.</param>
        /// <returns>Instance of Wait.</returns>
        public static Wait<ElementCollection<T>> WithReason<T>(this Wait<ElementCollection<T>> wait, string message)
        {
            wait.Reason = message;
            return wait;
        }

        /// <summary>
        /// Method to wait until specified conditions are fulfilled.
        /// </summary>
        /// <param name="wait">Instance of Wait.</param>
        /// <param name="conditions">Array of wait conditions.</param>
        public static void Until(this Wait<NetSelElement> wait, params IWaitCondition<NetSelElement>[] conditions)
        {
            Wait(wait, drv => conditions.EvaluateWaitConditions(wait.Element));
        }

        /// <summary>
        /// Method to wait until specified conditions are fulfilled.
        /// </summary>
        /// <param name="wait">Instance of Wait.</param>
        /// <param name="conditions">Array of wait conditions.</param>
        public static void Until(this Wait<ICollection<NetSelElement>> wait, params IWaitCondition<ICollection<NetSelElement>>[] conditions)
        {
            Wait(wait, drv => conditions.EvaluateWaitConditions(wait.Element));
        }

        /// <summary>
        /// Method to wait until collection contains elements.
        /// </summary>
        /// <param name="wait">Instance of Wait.</param>
        public static void UntilCollectionContainsElements<T>(this Wait<ElementCollection<T>> wait)
        {
            Wait(wait, drv => new WaitCondition<ElementCollection<T>>(elements => elements.GetElements().Length != 0).EvaluateWaitCondition(wait.Element));
        }

        /// <summary>
        /// Method to wait until collection does not contain any element.
        /// </summary>
        /// <param name="wait">Instance of Wait.</param>
        public static void UntilCollectionNotContainsElements<T>(this Wait<ElementCollection<T>> wait)
        {
            Wait(wait, drv => new WaitCondition<ElementCollection<T>>(elements => elements.GetElements().Length == 0).EvaluateWaitCondition(wait.Element));
        }
    }
}