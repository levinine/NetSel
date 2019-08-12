using System;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Handlers;
using Levi9.NetSel.Proxies;
using Levi9.NetSel.Unit.Tests.TestData;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests
{
    public class HandlerBuilderTests
    {
        private readonly HandlerBuilder _handlerBuilder;

        public HandlerBuilderTests()
        {
            _handlerBuilder = new HandlerBuilder();
        }

        [Fact]
        public void VerifyNewElementIsRegisteredTest()
        {
            _handlerBuilder.RegisterAdditionalType(typeof(TestHandler), proxy => new TestHandler(proxy));

            Assert.Null(Record.Exception(() => _handlerBuilder.BuildHandler(typeof(TestHandler), Mock.Of<IHandlerProxy>())));
        }

        [Fact]
        public void VerifyNonExistingElementThrowsExceptionTest()
        {
            Assert.Throws<NotSupportedException>(() => _handlerBuilder.BuildHandler(typeof(TestHandler), Mock.Of<IHandlerProxy>()));
        }

        [Fact]
        public void VerifyKnownElementIsBuiltTest()
        {
            _handlerBuilder.RegisterNetSelHandlerTypes();
            Assert.Null(Record.Exception(() =>_handlerBuilder.BuildHandler(typeof(NavigationHandler), new NavigationHandlerProxy(Mock.Of<IWebDriver>(), null, null))));
        }
    }
}
