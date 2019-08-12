using System;

namespace Levi9.NetSel.WaitConditions
{
    /// <summary>
    /// Contains methods specific for wait handling.
    /// </summary>
    public interface IWaitCondition<in T>
    {
        /// <summary>
        /// Returns wait condition.
        /// </summary>
        /// <returns>Function that represents wait condition.</returns>
        Func<T, bool> GetCondition();
    }
}