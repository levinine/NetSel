using System.IO;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Elements
{
    public class FileUploadElementTests
    {
        private readonly Mock<FileUploadElement> _fileUploadElement;

        public FileUploadElementTests()
        {
            _fileUploadElement = new Mock<FileUploadElement>(new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()));
            _fileUploadElement.Setup(x => x.WebElement).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifyUploadFileCallsCorrectMethodsTest()
        {
            _fileUploadElement.Object.UploadFile("testPath");
            _fileUploadElement.Verify(element => element.WebElement.SendKeys(Path.GetFullPath("testPath")), Times.Once);
        }
    }
}
