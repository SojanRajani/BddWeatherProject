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
    public class WeatherForecastTestSteps
    {
        private string Latitude;
        private string Longitude;

        private static string HomeUrl { get; set; }
        private static string SearchString = "temperature of ";

        private static int GoogleTemp { get; set; }
        private static int WeatherApiTemp { get; set; }

        private static DateTime RequestTime { get; set; }

        private IWebDriver webDriver { get; set; }
        private IWebElement searchBox { get; set; }
        private IWebElement searchButton { get; set; }

        private static ClientInterface weatherApi;

        [BeforeFeature]
        public static void Setup()
        {
            HomeUrl = ConfigurationManager.AppSettings["GoogleURL"];
            weatherApi = new ClientInterface();
        }

        [Given(@"I Call the google home page")]
        public void GivenICallTheGoogleHomePage()
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

        [When(@"I enter search string as : Current temperature of Location (.*)")]
        public void WhenIEnterSearchStringAsCurrentTemperatureOfLocation(string Location)
        {
            //Find the search box
            searchBox = webDriver.FindElement(By.ClassName("gLFyf"));
            //Pass in the search string
            searchBox.SendKeys(SearchBuilder(SearchString, Location));
            //Click on the search button
            searchButton = webDriver.FindElement(By.ClassName("gNO89b"));
            Thread.Sleep(1000);
            searchButton.Click();
            //Get the google temperature
            GoogleTemp = int.Parse(webDriver.FindElement(By.Id("wob_tm")).Text);
            webDriver.Close();
            webDriver.Quit();
        }

        [Given(@"I call Open weather api with Latitude (.*) and Longitude (.*)")]
        public void GivenICallOpenWeatherApiWithLatitudeAndLongitude(Decimal Latitude, Decimal Longitude)
        {
            this.Latitude = Latitude.ToString();
            this.Longitude = Longitude.ToString();

            WeatherResponseModel response = weatherApi.GetCurrentWeather(this.Latitude, this.Longitude);         

            WeatherApiTemp = Convert.ToInt32(response.current.temp);
        }

        [When(@"I enter search as : Current temperature of Location (.*) at Date Time (.*)")]
        public void WhenIEnterSearchAsCurrentTemperatureOfLocationAtDateTime(string location, string dateTime)
        {
            RequestTime = DateTime.ParseExact(dateTime, "dd/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            //Find the search box
            searchBox = webDriver.FindElement(By.ClassName("gLFyf"));
            //Pass in the search string
            searchBox.SendKeys(SearchBuilder_Time(SearchString, location, RequestTime.ToString()));
            //Click on the search button
            Thread.Sleep(1000);
            searchButton = webDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div[2]/div[1]/div[3]/center/input[1]"));
            searchButton.Click();
            //Get the google temperature
            GoogleTemp = int.Parse(webDriver.FindElement(By.Id("wob_tm")).Text);
            webDriver.Close();
            webDriver.Quit();
        }

        [Given(@"I call hourly Open weather api with Latitude (.*) and Longitude (.*)")]
        public void GivenICallHourlyOpenWeatherApiWithLatitudeAndLongitude(string latitude, string longtude)
        {
            var hourly = weatherApi.GetHourlyWeather(latitude, longtude);
            WeatherApiTemp = (int)hourly.Find(x => x.current.time.Hour == RequestTime.Hour).current.temp;
        }

        [Then(@"the current temperatures should be equal")]
        public void ThenTheCurrentTemperaturesShouldBeEqual()
        {
            Assert.IsTrue(Enumerable.Range(GoogleTemp - 2, GoogleTemp + 2).Contains(WeatherApiTemp));
        }


        static Func<string, string, string> SearchBuilder = (searchString, location) => new StringBuilder(searchString).Append(location).ToString();
        static Func<string, string, string, string> SearchBuilder_Time = (searchString, location, time) 
            => new StringBuilder(searchString)
                .Append(location)
                .Append(" at time ")
                .Append(time).ToString();
    }
}
