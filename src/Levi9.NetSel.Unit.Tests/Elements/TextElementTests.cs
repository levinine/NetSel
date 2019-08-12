using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class TextElementTests
    {
        private readonly Mock<TextElement> _textElement;

        public TextElementTests()
        {
            _textElement = new Mock<TextElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _textElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifyGetValueCallsCorrectMethodTest()
        {
            _textElement.Object.GetValue();
            _textElement.Verify(element => element.WebElement.GetAttribute("value"), Times.Once);
        }

        [Fact]
        public void VerifyGetTextCallsCorrectMethodTest()
        {
            _textElement.Object.GetText();
            _textElement.Verify(element => element.WebElement.Text, Times.Once);
        }
    }
}