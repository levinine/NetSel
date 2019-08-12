using System.Collections.ObjectModel;
using System.Linq;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class TableElementTests
    {
        private readonly Mock<TableElement> _checkBoxElement;

        public TableElementTests()
        {
            _checkBoxElement = new Mock<TableElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _checkBoxElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
            _checkBoxElement.Setup(x => x.WebElement.Text).Returns(It.IsAny<string>());
            _checkBoxElement.Setup(x => x.WebElement.FindElement(It.IsAny<By>())).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifyGetCellByContentCallsCorrectMethodsTest()
        {
            _checkBoxElement.Object.GetCellByContent("test");
            _checkBoxElement.Verify(element => element.WebElement.FindElement(By.XPath($"//td[contains(.,'test')]")), Times.Once);
        }

        [Fact]
        public void VerifyGetCellBySelectorCallsCorrectMethodsTest()
        {
            _checkBoxElement.Object.GetCellBySelector(It.IsAny<By>());
            _checkBoxElement.Verify(element => element.WebElement.FindElement(It.IsAny<By>()).Text, Times.Once);
        }

        [Fact]
        public void VerifyGetRowElementsCallsCorrectMethodsTest()
        {
            _checkBoxElement.Object.GetRowElements();
            _checkBoxElement.Verify(element => element.WebElement.FindElements(By.TagName("tr")), Times.Once);
        }

        [Fact]
        public void VerifyGetHeaderElementsCallsCorrectMethodsTest()
        {
            _checkBoxElement.Object.GetHeaderElements();
            _checkBoxElement.Verify(element => element.WebElement.FindElements(By.TagName("th")), Times.Once);
        }

        [Fact]
        public void VerifyGetRowSizeCallsCorrectMethodsTest()
        {
            _checkBoxElement.Setup(x => x.WebElement.FindElements(By.TagName("tr")))
                .Returns(new ReadOnlyCollection<IWebElement>(new[]{ Mock.Of<IWebElement>(), Mock.Of<IWebElement>()}.ToList()));

            Assert.Equal(2, _checkBoxElement.Object.GetRowSize());
        }

        [Fact]
        public void VerifyGetColumnSizeCallsCorrectMethodsTest()
        {
            _checkBoxElement.Setup(x => x.WebElement.FindElements(By.TagName("th")))
                .Returns(new ReadOnlyCollection<IWebElement>(new[] { Mock.Of<IWebElement>() }.ToList()));

            Assert.Equal(1, _checkBoxElement.Object.GetColumnSize());
        }
    }
}