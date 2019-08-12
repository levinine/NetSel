using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class ClickableElementTests
    {
        private readonly Mock<ClickableElement> _clickableElement;

        public ClickableElementTests()
        {
            _clickableElement = new Mock<ClickableElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _clickableElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifyClickCallsCorrectMethodsTest()
        {
            _clickableElement.Object.Click();
            _clickableElement.Verify(element => element.WebElement.Click(), Times.Once);
        }

        [Fact]
        public void VerifyIsDisplayedCallsCorrectMethodTest()
        {
            _clickableElement.Object.IsDisplayed();
            _clickableElement.Verify(element => element.WebElement.Displayed, Times.Once);
        }

        [Fact]
        public void VerifyIsEnabledCallsCorrectMethodTest()
        {
            _clickableElement.Object.IsEnabled();
            _clickableElement.Verify(element => element.WebElement.Enabled, Times.Once);
        }
    }
}