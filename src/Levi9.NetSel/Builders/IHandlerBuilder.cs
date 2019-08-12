using System;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Builders
{
    /// <summary>
    /// Contains methods for building handlers.
    /// </summary>
    public interface IHandlerBuilder
    {
        /// <summary>
        /// Builds handler object.
        /// </summary>
        /// <param name="handlerType">Type of handler.</param>
        /// <param name="proxy">Instance of IHandlerProxy.</param>
        /// <returns>Object.</returns>
        object BuildHandler(Type handlerType, IHandlerProxy proxy);
    }
}