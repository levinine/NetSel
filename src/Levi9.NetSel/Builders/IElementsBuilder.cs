using System;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Builders
{
    /// <summary>
    /// Contains methods for building elements.
    /// </summary>
    public interface IElementsBuilder
    {
        /// <summary>
        /// Builds new element.
        /// </summary>
        /// <param name="elementType">Type of element.</param>
        /// <param name="proxy">Instance of NetSelElementProxy.</param>
        /// <returns>Object.</returns>
        object BuildElement(Type elementType, NetSelElementProxy proxy);
        object BuildCompositeElement(Type elementType, NetSelElementProxy proxy);
    }
}