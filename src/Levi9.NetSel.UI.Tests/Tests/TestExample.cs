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
            //options.AddArgument("--headless");

            _driver = new ChromeDriver(options);
            _homePage = NetSel.PageFactory.CreatePage<PageExample>(_driver);
        }

        [Fact]
        public void TestCollectionWaitUntilNotPresent()
        {
            _homePage.Navigation.GoToPage();

            var ex = Assert.Throws<WebDriverTimeoutException>(() => _homePage.ButtonCollection.WaitFor(TimeSpan.FromSeconds(10)).WithReason("Something went wrong.").UntilCollectionNotContainsElements());
            Assert.Equal("Something went wrong.", ex.Message);
        }

        [Fact]
        public void TestElementWaitUntilClickable()
        {
            _homePage.Navigation.GoToPage();

            _homePage.ClickMeButton.WaitFor(TimeSpan.FromSeconds(5)).Until(Until.Clickable);
        }

        [Fact]
        public void TestElementFocus()
        {
            _homePage.Navigation.GoToPage();

            _homePage.NameInput.FocusElement();
            _homePage.BikeCheckbox.FocusElement();
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}