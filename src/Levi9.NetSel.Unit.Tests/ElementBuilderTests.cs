using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Levi9.NetSel.Builders;
using Levi9.NetSel.Elements;
using Levi9.NetSel.Proxies;
using Levi9.NetSel.Unit.Tests.TestData;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Levi9.NetSel.Unit.Tests
{
    public class ElementBuilderTests
    {
        private readonly ElementsBuilder _elementsBuilder;

        public ElementBuilderTests()
        {
            _elementsBuilder = new ElementsBuilder();
        }

        [Fact]
        public void VerifyNewElementIsRegisteredTest()
        {
            _elementsBuilder.RegisterAdditionalType(typeof(TestElement), proxy => new TestElement(proxy));

            Assert.Null(Record.Exception(() => _elementsBuilder.BuildElement(typeof(TestElement), new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()))));
        }

        [Fact]
        public void VerifyNonExistingElementThrowsExceptionTest()
        {
            Assert.Throws<NotSupportedException>(() => _elementsBuilder.BuildElement(typeof(TestElement), new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>())));
        }

        [Fact]
        public void VerifyKnownElementIsBuiltTest()
        {
            _elementsBuilder.RegisterNetSelTypes();
            Assert.Null(Record.Exception(() => _elementsBuilder.BuildElement(typeof(ClickableElement), new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()))));
        }

        [Fact]
        public void VerifyDefaultRegisteredElements()
        {
            _elementsBuilder.RegisterNetSelTypes();

            var referencedAssembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .FirstOrDefault(x => x.Name == "Levi9.NetSel");

            if (referencedAssembly == null)
                throw new NullReferenceException("Unable to find assembly: Levi9.NetSel");

            var assemblyTypes = Assembly.Load(referencedAssembly).GetTypes().Where(t => t.IsClass && t.Namespace == "Levi9.NetSel.Elements");

            foreach (var type in assemblyTypes.ToList())
            {
                if (type.IsDefined(typeof(CompilerGeneratedAttribute), false) || type == typeof(ElementCollection<>) || type == typeof(NetSelElement) || type == typeof(CompositeElement))
                    continue;

                Assert.Null(Record.Exception(() => _elementsBuilder.BuildElement(type, new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()))));
                Assert.Null(Record.Exception(() => _elementsBuilder.BuildElement(typeof(ElementCollection<>).MakeGenericType(type), new NetSelElementProxy(Mock.Of<IWebDriver>(), Mock.Of<By>()))));
            }
        }
    }
}
