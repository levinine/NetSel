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
            _homePage.Navigation.GoToPage();

            Assert.Throws<WebDriverTimeoutException>(() => _homePage.BodyContent.TableColumns.WaitFor(TimeSpan.FromSeconds(5)).UntilCollectionNotContainsElements());
        }

        [Fact]
        public void TestElementWaitUntilClickable()
        {
            _homePage.Navigation.GoToPage();
            Assert.Equal(15, _homePage.BodyContent.Banner.Links.GetElements().Length);
            Assert.Equal(9, _homePage.BodyContent.Banner.UnorderedList.Links.GetElements().Length);
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}