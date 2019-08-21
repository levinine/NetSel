# NetSel 2.0

[![Build status](https://ci.appveyor.com/api/projects/status/gov5i86kedol4ur7/branch/master?svg=true)](https://ci.appveyor.com/project/Milan/netsel-2307t/branch/master)
[![NuGet version](https://badge.fury.io/nu/Levi9.NetSel.svg)](https://badge.fury.io/nu/Levi9.NetSel)


**NetSel** is helping us writing Selenium test scripts using .Net in an easier way. 

NetSel is easy for setting up the project with defined elements and methods for manipulating with elements on pages and helpers that provide us with most used methods as Wait, take Screenshot and many more.

**Install and setup NetSel**

In order to create a new project, go to File -> New -> Project. Select Class Library, name the project and browse location and create.

Make sure your user is granted with the access to *https://nuget.com* and Package source in NuGet Package Manager for your solution is set to *https://www.nuget.org/api/v2*

In this way, you will be able to receive NetSel testing framework updates.

To add NetSel in your project by adding NuGet package:

    Right-click on References and Manage NuGet packages…
    Select above mentioned NuGet as the Package source
    Find Levi9.NetSel and install it

## Examples

#### Simple page and test example

```csharp
public class PageExample
{
    [Navigation(BaseUrl = "https://www.ultimateqa.com/", Path = "simple-html-elements-for-automation")]
    public NavigationHandler Navigation { get; set; }

    [Selector(Type = SelectorType.Id, Value = "button1")]
    public ClickableElement ClickMeButton { get; set; }

    [Selector(Type = SelectorType.Id, Value = "button1")]
    public ElementCollection<ClickableElement> ButtonCollection { get; set; }
}
```

```csharp
public class TestExample : IDisposable
{
    private readonly IWebDriver _driver;
    private readonly PageExample _homePage;

    public TestExample()
    {
        _driver = new ChromeDriver();
        _homePage = NetSel.PageFactory.CreatePage<PageExample>(_driver);
    }

    [Fact]
    public void TestCollectionWaitUntilNotPresent()
    {
        _homePage.Navigation.GoToPage();
        Assert.Throws<WebDriverTimeoutException>(() => _homePage.ButtonCollection.WaitFor(TimeSpan.FromSeconds(15)).UntilCollectionNotContainsElements());
    }

    [Fact]
    public void TestElementWaitUntilClickable()
    {
        _homePage.Navigation.GoToPage();
        _homePage.ClickMeButton.WaitFor(TimeSpan.FromSeconds(5)).Until(Until.Clickable);
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}
```

### Extensibility

NetSel framework is highly extensible and configurable. All custom handlers and elements can be registered and used within the framework. In the following examples, configuration code is called before each test for the sake of simplicity, but we suggest to move that code in your BaseTest class and call it only once for entire test runtime.

#### How to register a custom element

In order to register a custom element it is required to perform the following steps:
1. Create custom element class which extends `NetSelElement` class.
2. Register the custom element and configure `PageFactory`.

##### Creating custom element class

```csharp
public class CustomElement : NetSelElement
{
    public CustomElement(NetSelElementProxy proxy) : base(proxy){}

    public void CustomClick()
    {
        WebElement.Click();
    }
}
```

Custom elements can be used in the page just as any other elements that are provided by NetSel:
```csharp
public class PageWithCustomElementExample
{
    [Selector(Type = SelectorType.PartialLinkText, Value = "Create account")]
    public CustomElement CreateAccountLink { get; set; }

    [Selector(Type = SelectorType.PartialLinkText, Value = "Main Page")]
    public CustomElement MainPageLink { get; set; }

    …
}
```

##### Registering custom elements and configuring PageFactory

```csharp
public class TestWithCustomElementExample : IDisposable
{
    private readonly IWebDriver _driver;
    private readonly PageWithCustomElementExample _pageWithCustomElement;

    public TestWithCustomElementExample()
    {
        NetSel.PageFactory.Configure(configuration => new PageFactoryConfiguration
        {
            ElementsBuilder = new ElementsBuilder()
                .RegisterNetSelTypes()
                .RegisterAdditionalType(typeof(CustomElement), proxy => new CustomElement(proxy))
        }.ConfigureNetSelHandlerBuilder().ConfigurePageCreation());

        _driver = new ChromeDriver();
        _pageWithCustomElement = NetSel.PageFactory.CreatePage<PageWithCustomElementExample>(_driver);
    }

    [Fact]
    public void NavigateToCreateAccountPageTest()
    …
}
```