using System;

namespace Levi9.NetSel.WaitConditions
{
    /// <inheritdoc />
    public class WaitCondition<T> : IWaitCondition<T>
    {
        /// <summary>
        /// Initializes a new instance of the WaitCondition class.
        /// </summary>
        /// <param name="condition">Wait condition.</param>
        public WaitCondition(Func<T, bool> condition)
        {
            Condition = condition;
        }

        private Func<T, bool> Condition { get; }
        
        /// <summary>
        /// Returns condition function for WaitExtensionMethods.
        /// </summary>
        /// <returns>Condition function.</returns>
        public Func<T, bool> GetCondition()
        {
            return Condition;
        }
    }
}