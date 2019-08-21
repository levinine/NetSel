using System;
using System.Collections.Generic;
using Levi9.NetSel.Handlers;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Builders
{
    /// <inheritdoc />
    public class HandlerBuilder : IHandlerBuilder
    {
        private readonly Dictionary<Type, Func<IHandlerProxy, object>> _supportedTypesCatalog = new Dictionary<Type, Func<IHandlerProxy, object>>();

        /// <summary>
        /// Registers NetSel handler types.
        /// </summary>
        /// <returns>HandlerBuilder.</returns>
        public HandlerBuilder RegisterNetSelHandlerTypes()
        {
            _supportedTypesCatalog.Add(typeof(NavigationHandler), proxy => new NavigationHandler((NavigationHandlerProxy)proxy));
            _supportedTypesCatalog.Add(typeof(WindowHandler), proxy => new WindowHandler((WindowHandlerProxy)proxy));
            return this;
        }

        /// <summary>
        /// Method to register additional handlers.
        /// </summary>
        /// <param name="type">Handler type.</param>
        /// <param name="newInstanceFunc">Delegate responsible for creating new instance of handler.</param>
        /// <returns>HandlerBuilder</returns>
        public HandlerBuilder RegisterAdditionalType(Type type, Func<IHandlerProxy, object> newInstanceFunc)
        {
            _supportedTypesCatalog.Add(type, newInstanceFunc);
            return this;
        }

        /// <summary>
        /// Builds handler object.
        /// </summary>
        /// <param name="handlerType">Type of handler.</param>
        /// <param name="proxy">Instance of IHandlerProxy.</param>
        /// <returns>Object.</returns>
        public object BuildHandler(Type handlerType, IHandlerProxy proxy)
        {
            if (!_supportedTypesCatalog.ContainsKey(handlerType))
                throw new NotSupportedException($"Type {handlerType} is not supported");

            return _supportedTypesCatalog[handlerType].Invoke(proxy);
        }
    }
}