using System.Drawing;
using Levi9.NetSel.Proxies;
using OpenQA.Selenium;

namespace Levi9.NetSel.Handlers
{
    /// <summary>
    /// Contains support methods for handling window events.
    /// </summary>
    public class WindowHandler
    {
        private readonly IWebDriver _webDriver;

        /// <summary>
        /// Initializes a new instance of the WindowHandler class.
        /// </summary>
        /// <param name="proxy">Instance of WindowHandlerProxy.</param>
        public WindowHandler(WindowHandlerProxy proxy)
        {
            _webDriver = proxy.WebDriver;
        }

        /// <summary>
        /// Switches to the tab by passing the part of URL of wanted tab.
        /// </summary>
        /// <param name="textPartUrl">URL part of wanted tab.</param>
        public void SwitchToTab(string textPartUrl)
        {
            var handles = _webDriver.WindowHandles;

            foreach (var handle in handles)
            {
                if (_webDriver.SwitchTo().Window(handle).Url.Contains(textPartUrl))
                {
                    _webDriver.SwitchTo().Window(handle);
                    break;
                }
            }
        }

        /// <summary>
        /// Method used for capturing images in current window.
        /// </summary>
        /// <param name="fullPath">Path where to save file.</param>
        /// <param name="format">File format.</param>
        public void CaptureScreenshot(string fullPath, ScreenshotImageFormat format)
        {
            if (!(_webDriver is ITakesScreenshot screenshotDriver))
                return;
            var screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(fullPath, format);
        }

        /// <summary>
        /// Maximizes currently opened window.
        /// </summary>
        public void MaximizeBrowser()
        {
            _webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Changes current window size.
        /// </summary>
        /// <param name="width">Desired width.</param>
        /// <param name="height">Desired height.</param>
        public void ResizeBrowser(int width, int height)
        {
            _webDriver.Manage().Window.Size = new Size(width, height);
        }
    }
}