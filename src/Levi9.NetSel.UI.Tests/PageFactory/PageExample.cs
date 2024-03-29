﻿using Levi9.NetSel.Attributes;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Handlers;

namespace Levi9.NetSel.UI.Tests.PageFactory
{
    public class PageExample
    {
        [Navigation(BaseUrl = "https://www.ultimateqa.com/", Path = "simple-html-elements-for-automation")]
        public NavigationHandler Navigation { get; set; }

        [Selector(Type = SelectorType.Id, Value = "button1")]
        public ClickableElement ClickMeButton { get; set; }

        [Selector(Type = SelectorType.Id, Value = "et_pb_contact_name_0")]
        public InputElement NameInput { get; set; }

        [Selector(Type = SelectorType.CssSelector, Value = "input[value='Bike']")]
        public SelectWebElement BikeCheckbox { get; set; }

        [Selector(Type = SelectorType.Id, Value = "button1")]
        public ElementCollection<ClickableElement> ButtonCollection { get; set; }
    }
}