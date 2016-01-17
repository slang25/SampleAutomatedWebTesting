using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Shouldly;

namespace AutomatedTests
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void TitleIsCorrect()
        {
            for (int i = 0; i < 30; i++)
            {
                //IWebDriver webDriver = new ChromeDriver();
                //IWebDriver webDriver = new PhantomJSDriver();
                // TODO ^^ So instead of this we do this:
                IWebDriver webDriver = new RemoteWebDriver(new Uri("http://localhost:4444"), new DesiredCapabilities());
                // ^^ this is how you'd call a Selenium Grid server, the desired capability is used to ask for a particular browser (or set of configuration)
                // you will need to first run phantomjs.exe --webdriver=4444
                // you might run a local instance simply by calling java -jar selenium-grid.jar or something, then register browsers with it
                // you could also call source labs, browserstack or whatever

                // When using ChromeDriver you could try reuisng a webdriver session,
                // but then you could get failing tests interfering with other tests, you can do some work to mitigation this and use things like cookejars to isolate tests
                // but with PhantomJS giving you a new session each time, you don't have to worry about it.

                // we had it so that failures would output a screenshot (that would get attached to the test result), this is available through the webdriver api and is support by PhantomJS
                // we also had the webdriver switchable by config, so that we could quickly switch to a full browser to troubleshoot bugs

                webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));


                webDriver.Navigate().GoToUrl("http://localhost:3988/");
                var header = webDriver.FindElement(By.CssSelector("div.jumbotron > h1"));
                header.Text.ShouldBe("ASP.NET");
            }

            //var liCount = _webDriver.FindElement(By.Id("test-content")).FindElements(By.TagName("li")).Count;

            //liCount.ShouldBe(3);

            // This sort of fiddly stuff, i.e. elements with timeouts, domReady etc... are not as bad as I remember.
            // because FindElements will wait using the _webdriver implicit wait settings (set when we construct the WebDriver object)
            // but what if there was already an <li> in the list, it fails! what if the dom exists, but then we replace it with ajax

            // thats when you might do something explicit like this:

            //var fiveSecondWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            //fiveSecondWait.Until(wd =>
            //{
            //    // do some checks
            //    return true;
            //});

            // We used to queue some javascript so that we know that all synchrounous task had been parse and completed by the browser and we were ready to rock
        }

    }
}
