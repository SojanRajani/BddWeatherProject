using Bdd.Project.Test.ApiClients;
using Bdd.Project.Test.Models;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Weather.Test.Steps
{
    [Binding]
    public class CurrencyConversionTestSteps
    {
        private string baseCurrency;
        private string toCurrency;
        private string rates;

        [Given(@"Currency converter Api Endpoint")]
        public void GivenCurrencyConverterApiEndpoint()
        {
        }
        
        [When(@"I enter search string as : (.*) to (.*) current rates")]
        public void WhenIEnterSearchStringAsToCurrentRates(string baseCurrency, string toCurrency)
        {
            this.baseCurrency = baseCurrency;
            this.toCurrency = toCurrency;
            ClientInterface currencyconverter = new ClientInterface();
            CurrencyConversionResponse response = currencyconverter.GetCurrencyCoverted(baseCurrency, toCurrency);
            this.rates = Math.Round(response.result,2).ToString();
        }

        [Then(@"the current rates shown as (.*)")]
        public void ThenTheCurrentRatesShownAs(string rates)
        {
            this.rates.Should().Be(rates);

        }
    }
}
