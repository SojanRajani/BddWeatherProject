using Bdd.Project.Test.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using OpenWeatherApi;
using System.Configuration;
using CurrencyConverterClient;
using System.Collections.Generic;
using System.Linq;

namespace Bdd.Project.Test.ApiClients
{
    public class ClientInterface
    {
        public static WeatherResponseModel weather;
        public static CurrencyConversionResponse currency;

        private static string AppId = ConfigurationManager.AppSettings["ApiAppId"];
        private static string CurrencyAppId = ConfigurationManager.AppSettings["CurrencyApiAppId"];

        public WeatherResponseModel GetCurrentWeather(string lat, string lon)
        {
            Client client = new Client(new HttpClient());
            var response = client.CurrentWeatherDataAsync(q: "", id: "", lat: lat, lon: lon, zip: "", units: Units.Metric, lang: Lang.En, mode: Mode.Json, AppId: AppId).Result;
            weather = new WeatherResponseModel()
            {
                current = new Current()
                {
                    temp = response.Main.Temp
                }
            };
            return weather;
        }

        public CurrencyConversionResponse GetCurrencyCoverted(string baseCurrency, IEnumerable<string> Symbols)
        {
            CurrencyConverterClientClass client = new CurrencyConverterClientClass(new HttpClient());
            var response = client.GetLatestAsync(baseCurrency, Symbols, CurrencyAppId).Result;
            currency = new CurrencyConversionResponse()
            {
                result = response.Rates1[Symbols.FirstOrDefault()]
            };
            return currency;
        }
    }
}
