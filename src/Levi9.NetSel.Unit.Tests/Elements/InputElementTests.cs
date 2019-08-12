using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class InputElementTests
    {
        private readonly Mock<InputElement> _inputElement;

        public InputElementTests()
        {
            _inputElement = new Mock<InputElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _inputElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifySendKeysCallsCorrectMethodsTest()
        {
            _inputElement.Object.SendKeys(string.Empty);
            _inputElement.Verify(element => element.WebElement.SendKeys(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyClearAndSendKeysCallsCorrectMethodTest()
        {
            _inputElement.Object.ClearAndSendKeys(string.Empty);
            _inputElement.Verify(element => element.WebElement.Clear(), Times.Once);
            _inputElement.Verify(element => element.WebElement.SendKeys(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyClearFieldCallsCorrectMethodsTest()
        {
            _inputElement.Object.ClearField();
            _inputElement.Verify(element => element.WebElement.Clear(), Times.Once);
        }
    }
}