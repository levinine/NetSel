using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class RadioButtonElementTests
    {
        private readonly Mock<RadioButtonElement> _radioButtonElement;

        public RadioButtonElementTests()
        {
            _radioButtonElement = new Mock<RadioButtonElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _radioButtonElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifySelectValueCallsCorrectMethodsTest()
        {
            _radioButtonElement.Object.SelectValue();
            _radioButtonElement.Verify(element => element.WebElement.Click(), Times.Once);
        }

        [Fact]
        public void VerifyIsSelectedCallsCorrectMethodTest()
        {
            _radioButtonElement.Object.IsSelected();
            _radioButtonElement.Verify(element => element.WebElement.Selected, Times.Once);
        }

        [Fact]
        public void VerifyGetValueCallsCorrectMethodTest()
        {
            _radioButtonElement.Object.GetValue();
            _radioButtonElement.Verify(element => element.WebElement.GetAttribute("value"), Times.Once);
        }
    }
}