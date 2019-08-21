using System;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Extensions;
using Levi9.NetSel.UI.Tests.Elements;
using Levi9.NetSel.UI.Tests.PageFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Levi9.NetSel.UI.Tests.Tests
{
    public class TestWithCustomElementExample : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly PageWithCustomElementExample _pageWithCustomElement;

        public TestWithCustomElementExample()
        {
            NetSel.PageFactory.Configure(configuration => new PageFactoryConfiguration
            {
                ElementsBuilder = new ElementsBuilder()
                    .RegisterNetSelTypes()
                    .RegisterAdditionalType(typeof(CustomElement), proxy => new CustomElement(proxy))
            }.ConfigureNetSelHandlerBuilder().ConfigurePageCreation());

            _driver = new ChromeDriver();
            _pageWithCustomElement = NetSel.PageFactory.CreatePage<PageWithCustomElementExample>(_driver);
        }

        [Fact]
        public void NavigateToCreateAccountPageTest()
        {
            _pageWithCustomElement.Navigation.GoToPage();
            _pageWithCustomElement.Window.MaximizeBrowser();
            _pageWithCustomElement.MainPageLink.WaitFor(TimeSpan.FromSeconds(10)).Until(Until.Visible);

            _pageWithCustomElement.CreateAccountLink.CustomClick();

            Assert.False(_pageWithCustomElement.CreateAccountLink.CustomIsPresent());
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
