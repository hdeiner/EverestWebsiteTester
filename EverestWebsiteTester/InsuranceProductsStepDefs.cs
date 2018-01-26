using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;
using System;
using System.Collections.ObjectModel;
using Xunit;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace InsuranceProducts
{
    [Binding]
    public class InsuranceProductsStepDefs : IDisposable
    {
        private string baseURL = "https://www.everestre.com";
        private IWebDriver webDriver;
        private WebDriverWait webDriverWait;

        public InsuranceProductsStepDefs()
        {
            webDriver = new ChromeDriver();
//            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            webDriverWait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
        }

        [Given(@"I navigate to the Everest Website")]
        public void GivenINavigateToTheEverestWebsite()
        {
            webDriver.Navigate().GoToUrl(this.baseURL);
            webDriverWait.Until(webDriver => webDriver.FindElement(By.Id("Insurance1")));
            SaveScreenshot();
        }

        [Given(@"I examine the insurance menu")]
        public void GivenIExamineTheInsuranceMenu()
        {
            IWebElement insuranceMenu = webDriver.FindElement(By.Id("Insurance1"));
            Actions action = new Actions(webDriver);
            action.MoveToElement(insuranceMenu).MoveToElement(insuranceMenu).Build().Perform();
            SaveScreenshot();

            ReadOnlyCollection<IWebElement> productsSubMenu = webDriver.FindElements(By.XPath("//div[contains(@id,'Products')]"));
            Assert.True(productsSubMenu.Count == 1);
        }

        [When(@"I select the insurance products")]
        public void WhenISelectTheInsuranceProducts()
        {
            ReadOnlyCollection<IWebElement> productsSubMenu = webDriver.FindElements(By.XPath("//div[contains(@id,'Products')]"));
            Actions action = new Actions(webDriver);
            action.MoveToElement(productsSubMenu[0]).MoveToElement(productsSubMenu[0]).Build().Perform();
            SaveScreenshot();

            ReadOnlyCollection<IWebElement> productsSideMenu = webDriver.FindElements(By.XPath("//div[contains(@id,'Products')]"));
            Assert.True(productsSideMenu.Count > 1);
        }

        [Then(@"I should see the insurance product called ""(.*)"" on line ""(.*)""")]
        public void ThenIShouldSeeTheInsuranceProductCalledOnLine(string expectedMenuName, int expectedMenuLineNumber)
        {
            ReadOnlyCollection<IWebElement> subSubMenu = webDriver.FindElements(By.ClassName("subsub"));
            Assert.True(subSubMenu.Count == 1);

            int actualMenuLineNumber = 0;
            foreach (var elem in subSubMenu[0].FindElements(By.XPath(".//*[@type='div']")))
            {
                Assert.Equal(expectedMenuName, elem.Text);
                Assert.Equal(expectedMenuLineNumber, ++actualMenuLineNumber);
            }

        }

        public void Dispose()
        {
            webDriver.Quit();
        }
        
        private void SaveScreenshot()
        {
            string fileName = "./" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg";
            Screenshot screenShot = ((ITakesScreenshot)webDriver).GetScreenshot();
            screenShot.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
        }
    }
}
