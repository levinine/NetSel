using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class CheckBoxElementTests
    {
        private readonly Mock<CheckBoxElement> _checkBoxElement;

        public CheckBoxElementTests()
        {
            _checkBoxElement = new Mock<CheckBoxElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _checkBoxElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifyCheckCallsCorrectMethodsTest()
        {
            _checkBoxElement.Object.Check();
            _checkBoxElement.Verify(element => element.WebElement.Selected, Times.Once);
            _checkBoxElement.Verify(element => element.WebElement.Click(), Times.Once);
        }

        [Fact]
        public void VerifyIsCheckedCallsCorrectMethodTest()
        {
            _checkBoxElement.Object.IsChecked();
            _checkBoxElement.Verify(element => element.WebElement.Selected, Times.Once);
        }

        [Fact]
        public void VerifyUncheckCallsCorrectMethodsTest()
        {
            _checkBoxElement.Setup(x => x.WebElement.Selected).Returns(true);
            _checkBoxElement.Object.Uncheck();
            _checkBoxElement.Verify(element => element.WebElement.Selected, Times.Once);
            _checkBoxElement.Verify(element => element.WebElement.Click(), Times.Once);
        }

        [Fact]
        public void VerifyGetValueCallsCorrectMethodTest()
        {
            _checkBoxElement.Object.GetValue();
            _checkBoxElement.Verify(element => element.WebElement.GetAttribute("value"), Times.Once);
        }

        [Fact]
        public void VerifyIsEnabledCallsCorrectMethodTest()
        {
            _checkBoxElement.Object.IsEnabled();
            _checkBoxElement.Verify(element => element.WebElement.Enabled, Times.Once);
        }
    }
}
