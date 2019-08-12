using System;
using System.Configuration;
using Levi9.NetSel.Handlers;
using Levi9.NetSel.Proxies;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests.Handlers
{
    public class NavigationHandlerTests : IDisposable
    {
        private NavigationHandler _navigationHandler;
        private readonly Mock<IWebDriver> _driverMock;

        public NavigationHandlerTests()
        {
            _driverMock = new Mock<IWebDriver>();
            _driverMock.Setup(x => x.Navigate()).Returns(Mock.Of<INavigation>());
        }

        [Fact]
        public void VerifyBaseUrlIsLoadedFromConfigTest()
        {
            _navigationHandler = new NavigationHandler(new NavigationHandlerProxy(_driverMock.Object, null, null));
            _navigationHandler.NavigateTo();

            _driverMock.Verify(x => x.Navigate().GoToUrl("test"), Times.Once);
        }

        [Fact]
        public void VerifyBaseUrlFromConstructorPriorityTest()
        {
            _navigationHandler = new NavigationHandler(new NavigationHandlerProxy(_driverMock.Object, "test2", null));
            _navigationHandler.NavigateTo();

            _driverMock.Verify(x => x.Navigate().GoToUrl("test2"), Times.Once);
        }

        [Fact]
        public void VerifyPathFromConstructorIsAppliedTest()
        {
            _navigationHandler = new NavigationHandler(new NavigationHandlerProxy(_driverMock.Object, null, "/demo"));
            _navigationHandler.NavigateTo();

            _driverMock.Verify(x => x.Navigate().GoToUrl("test/demo"), Times.Once);
        }

        [Fact]
        public void VerifyBaseUrlAndPathFromConstructorAreAppliedTest()
        {
            _navigationHandler = new NavigationHandler(new NavigationHandlerProxy(_driverMock.Object, "test2", "/demo"));
            _navigationHandler.NavigateTo();

            _driverMock.Verify(x => x.Navigate().GoToUrl("test2/demo"), Times.Once);
        }

        public void Dispose()
        {
            ConfigurationManager.AppSettings["baseUrl"] = "test";
        }
    }
}
