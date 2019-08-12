using System;
using Levi9.NetSel.Extensions;
using Levi9.NetSel.UI.Tests.PageFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Levi9.NetSel.UI.Tests.Tests
{
    public sealed class TestExample : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly PageExample _homePage;

        public TestExample()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            _driver = new ChromeDriver(options);
            _homePage = NetSel.PageFactory.CreatePage<PageExample>(_driver);
        }

        [Fact]
        public void TestCollectionWaitUntilNotPresent()
        {
            _homePage.Navigation.NavigateTo();

            Assert.Throws<WebDriverTimeoutException>(() => _homePage.ButtonCollection.WaitFor(TimeSpan.FromSeconds(15)).UntilCollectionNotContainsElements());
        }

        [Fact]
        public void TestElementWaitUntilClickable()
        {
            _homePage.Navigation.NavigateTo();

            _homePage.ClickMeButton.WaitFor(TimeSpan.FromSeconds(5)).Until(Until.Clickable);
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}