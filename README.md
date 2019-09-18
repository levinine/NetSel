# NetSel 2.0

[![Build status](https://ci.appveyor.com/api/projects/status/pmahsjio4s9kom78/branch/master?svg=true)](https://ci.appveyor.com/project/uxi187/netsel/branch/master)
[![NuGet version](https://badge.fury.io/nu/Levi9.NetSel.svg)](https://badge.fury.io/nu/Levi9.NetSel)


**NetSel** is OpenSource Test Automation framework developed within Levi9, based on Selenium and C#, which enables common/frequently used tests collections creation and tests standardization. Framework has modular architecture which is based on idea of code reuse, encapsulation and object-oriented concepts.  

NetSel is easy for setting up the project with defined elements and methods for manipulating with elements on pages and helpers that provide us with most used methods as Wait, take Screenshot and many more.

NetSel can be added as a _Nuget package_ to the existing project or can be obtained through _NetSel project template_. 

NetSel project template is distributed as a Visual Studio extension.

##### Use NetSel in your project by adding NuGet package

Make sure your user is granted with the access to *https://nuget.com* and Package source in NuGet Package Manager for your solution is set to *https://www.nuget.org/api/v2*

    Right-click on References and Manage NuGet packages…
    Select above mentioned NuGet as the Package source
    Find Levi9.NetSel and install it

##### Use NetSel by creating project from template

    Right-click on Extensions and search for `NetSel`
    Download and install Levi9.NetSel.Template.

To create a new project, go to File -> New -> Project. Select installed template, name the project and browse location and create.

NetSel template comes with ChromeDriver installed. Other driver can be installed via NuGet Package Manager:
    
	Right-click on References and Manage NuGet packages… for desired project/solution
    Search for desired driver (_e.g. GeckoDriver_) and install it

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

##### Registering custom elements and configuring PageFactory

```csharp
public class TestWithCustomElementExample
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

Now, when steps above have been finalized, the custom element may be used in a simple manner same as using any other NetSel element in the page:
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
