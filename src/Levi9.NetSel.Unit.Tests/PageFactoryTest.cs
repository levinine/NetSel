using System;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Extensions;
using Levi9.NetSel.Unit.Tests.TestData;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests
{
    public class PageFactoryTest : IDisposable
    {
        private readonly Mock<IWebDriver> _mockDriver;

        public PageFactoryTest()
        {
            _mockDriver = new Mock<IWebDriver>();
            _mockDriver.Setup(x => x.Navigate()).Returns(Mock.Of<INavigation>());
            _mockDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(Mock.Of<IWebElement>());
        }

        [Fact]
        public void VerifyDefaultPageFactoryConfigurationIsAppliedTest()
        {
            Assert.NotNull(PageFactory.GetConfiguration().ElementsBuilder);
            Assert.NotNull(PageFactory.GetConfiguration().HandlerBuilder);
            Assert.NotNull(PageFactory.GetConfiguration().CreatePageAction);
        }

        [Fact]
        public void VerifyCustomConfigurationCanBeProvidedTest()
        {
            PageFactory.Configure(configuration => new PageFactoryConfiguration().ConfigureNetSelElementsBuilder());
            Assert.NotNull(PageFactory.GetConfiguration().ElementsBuilder);
            Assert.Null(PageFactory.GetConfiguration().HandlerBuilder);
            Assert.Null(PageFactory.GetConfiguration().CreatePageAction);
        }

        [Fact]
        public void VerifyPageInstanceIsCreatedTest()
        {
            var elementsBuilder = new ElementsBuilder();
            elementsBuilder.RegisterAdditionalType(typeof(TestElement), proxy => new TestElement(proxy));

            var pageFactoryConfiguration = new PageFactoryConfiguration {ElementsBuilder = elementsBuilder};

            PageFactory.Configure(configuration => pageFactoryConfiguration.ConfigureNetSelHandlerBuilder().ConfigurePageCreation());

            var page = PageFactory.CreatePage<TestPage>(_mockDriver.Object);

            page.TestNavigation.NavigateTo();
            _mockDriver.Verify(x => x.Navigate().GoToUrl("test/demo"), Times.Once);

            page.TestElement.WebElement.Submit();
            _mockDriver.Verify(x => x.FindElement(By.ClassName("test")), Times.Once);
        }

        public void Dispose()
        {
            PageFactory.Configure(configuration => new PageFactoryConfiguration()
                .ConfigureNetSelElementsBuilder()
                .ConfigureNetSelHandlerBuilder()
                .ConfigurePageCreation());
        }
    }
}
