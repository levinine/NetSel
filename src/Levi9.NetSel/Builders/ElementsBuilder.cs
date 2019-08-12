using System;
using System.Collections.Generic;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;

namespace Levi9.NetSel.Builders
{
    /// <inheritdoc />
    public class ElementsBuilder : IElementsBuilder
    {
        private readonly Dictionary<Type, Func<NetSelElementProxy, object>> _supportedTypesCatalog = new Dictionary<Type, Func<NetSelElementProxy, object>>();

        /// <summary>
        /// Registers NetSel element types.
        /// </summary>
        public void RegisterNetSelTypes()
        {
            _supportedTypesCatalog.Add(typeof(CheckBoxElement), proxy => new CheckBoxElement(proxy));
            _supportedTypesCatalog.Add(typeof(ClickableElement), proxy => new ClickableElement(proxy));
            _supportedTypesCatalog.Add(typeof(FileUploadElement), proxy => new FileUploadElement(proxy));
            _supportedTypesCatalog.Add(typeof(InputElement), proxy => new InputElement(proxy));
            _supportedTypesCatalog.Add(typeof(RadioButtonElement), proxy => new RadioButtonElement(proxy));
            _supportedTypesCatalog.Add(typeof(HiddenFieldElement), proxy => new HiddenFieldElement(proxy));
            _supportedTypesCatalog.Add(typeof(SelectWebElement), proxy => new SelectWebElement(proxy));
            _supportedTypesCatalog.Add(typeof(TableElement), proxy => new TableElement(proxy));
            _supportedTypesCatalog.Add(typeof(TextElement), proxy => new TextElement(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<CheckBoxElement>), proxy => new ElementCollection<CheckBoxElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<ClickableElement>), proxy => new ElementCollection<ClickableElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<FileUploadElement>), proxy => new ElementCollection<FileUploadElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<InputElement>), proxy => new ElementCollection<InputElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<RadioButtonElement>), proxy => new ElementCollection<RadioButtonElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<HiddenFieldElement>), proxy => new ElementCollection<HiddenFieldElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<SelectWebElement>), proxy => new ElementCollection<SelectWebElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<TableElement>), proxy => new ElementCollection<TableElement>(proxy));
            _supportedTypesCatalog.Add(typeof(ElementCollection<TextElement>), proxy => new ElementCollection<TextElement>(proxy));
        }

        /// <summary>
        /// Method to register additional elements.
        /// </summary>
        /// <param name="type">Element type.</param>
        /// <param name="newInstanceFunc">Delegate responsible for creating new instance of NetSel element.</param>
        public void RegisterAdditionalType(Type type, Func<NetSelElementProxy, object> newInstanceFunc)
        {
            _supportedTypesCatalog.Add(type, newInstanceFunc);
        }

        /// <summary>
        /// Builds NetSel element.
        /// </summary>
        /// <param name="elementType">Type of element.</param>
        /// <param name="proxy">Instance of NetSelElementProxy.</param>
        /// <returns>Object.</returns>
        public object BuildElement(Type elementType, NetSelElementProxy proxy)
        {
            if(!_supportedTypesCatalog.ContainsKey(elementType))
                throw new NotSupportedException($"Type {elementType} is not supported");

            return _supportedTypesCatalog[elementType].Invoke(proxy);
        }
    }
}