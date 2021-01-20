using Bdd.Project.Test.ApiClients;
using Bdd.Project.Test.Models;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace Weather.Test.Steps
{
    [Binding]
    public class WeatherForecastTestSteps
    {
        private string baseurl;
        private string Location;
        private string Latitude;
        private string Longitude;
        private int currenttemperature;

        [Given(@"the google url (.*)")]
        public void GivenTheGoogleUrl(string baseurl)
        {
            this.baseurl = baseurl;
        }

        [Given(@"Open Weather Api Endpoint")]
        public void GivenOpenWeatherApiEndpoint()
        {
        }

        [When(@"I enter search string as : Current temperature of Location (.*)")]
        public void WhenIEnterSearchStringAsCurrentTemperatureOfLocation(string Location)
        {
            this.Location = Location;
        }

        [When(@"I give Latitude (.*) and Longitude (.*)")]
        public void WhenIGiveLatitudeAndLongitude(Decimal Latitude, Decimal Longitude)
        {
            this.Latitude = Latitude.ToString();
            this.Longitude = Longitude.ToString();
            ClientInterface weatherApi = new ClientInterface();
            WeatherResponseModel response = weatherApi.GetCurrentWeather(this.Latitude, this.Longitude);
            this.currenttemperature = Convert.ToInt32(response.current.temp);
        }

        [Then(@"the current temperature shown as (.*)")]
        public void ThenTheCurrentTemperatureShownAs(int temperature)
        {
            this.currenttemperature.Should().BeGreaterThan(temperature);
        }
    }
}
