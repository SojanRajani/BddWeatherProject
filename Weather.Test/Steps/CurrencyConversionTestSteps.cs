using Bdd.Project.Test.ApiClients;
using Bdd.Project.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace Weather.Test.Steps
{
    [Binding]
    public class CurrencyConversionTestSteps
    {
        private static double GoogleRate { get; set; }
        private static double rates;

        private IWebDriver webDriver { get; set; }
        private IWebElement searchBox { get; set; }
        private IWebElement searchButton { get; set; }

        private static string HomeUrl { get; set; }
        private static string SearchString = "Convert Currency  ";

        [BeforeFeature]
        public static void Setup()
        {
            HomeUrl = ConfigurationManager.AppSettings["GoogleURL"];
        }

        [Given(@"Call Google home URL")]
        public void GivenCallGoogleHomeURL()
        {
            // Initialising Chrome driver
            ChromeOptions options = new ChromeOptions()
            {
                AcceptInsecureCertificates = true
            };

            webDriver = new ChromeDriver(options);
            // Navigating to Google home
            webDriver.Navigate().GoToUrl(HomeUrl);

            webDriver.Manage().Window.Maximize();
        }


        [Then(@"Enter search box text: Convert (.*) to (.*)")]
        public void ThenEnterSearchBoxTextConvertTo(string baseCurrency, string toCurrency)
        {
            //Find the search box
            searchBox = webDriver.FindElement(By.ClassName("gLFyf"));
            //Pass in the search string
            searchBox.SendKeys(SearchBuilder(SearchString, baseCurrency, toCurrency));
            //Click on the search button
            searchButton = webDriver.FindElement(By.ClassName("gNO89b"));
            Thread.Sleep(1000);
            searchButton.Click();

            GoogleRate = double.Parse(webDriver.FindElement(By.XPath("/ html / body / div[7] / div[2] / div[9] / div[2] / div / div[2] / div[2] / div / div / div[1] / div / div / div / div / div / div[1] / div[1] / div[2] / span[1]")).Text);
            
            webDriver.Close();
            webDriver.Quit();
        }

        [Then(@"I call Currency Convertor with (.*) and (.*)")]
        public void ThenICallCurrencyConvertorWithINRAndUSD(string baseCurrency, string toCurrency)
        {
            ClientInterface currencyconverter = new ClientInterface();
            CurrencyConversionResponse response = currencyconverter.GetCurrencyCoverted(baseCurrency, toCurrency);
            rates = (Math.Round(response.result, 3));
        }

        [Then(@"the conversion rate should be equal")]
        public void ThenTheConversionRateShouldBeEqual()
        {
            Assert.IsTrue(IsInRange(GoogleRate, rates));
        }

        //Helper functions
        static Func<double, double, bool> IsInRange = (GoogleRate, rates) => rates > GoogleRate - 0.5 && rates < GoogleRate + 0.5;
        static Func<string, string, string, string> SearchBuilder = (searchString, baseCR, toCR) => new StringBuilder(searchString).Append(baseCR).Append(" to ").Append(toCR).ToString();

    }
}
