using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace WindowsFormsApp1.Tests
{
    public class EbaySearchTests : IDisposable
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public EbaySearchTests()
        {
            FirefoxOptions options = new FirefoxOptions();
            driver = new FirefoxDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Test1_field()
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.ebay.com");

                var searchBox = driver.FindElement(By.Id("gh-ac"));
                Assert.NotNull(searchBox);
            }
            catch (Exception ex)
            {
                Assert.Fail("Kļūda pārbaudot meklēšanas lauku: " + ex.Message);
            }
        }

        [Fact]
        public void Test2_search()
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.ebay.com");

                var searchButton = driver.FindElement(By.Id("gh-btn"));
                Assert.NotNull(searchButton); 
            }
            catch (Exception ex)
            {
                Assert.Fail("Kļūda pārbaudot meklēšanas pogu: " + ex.Message);
            }
        }

        public void Dispose()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
