using Bdd.Project.Test.Models;
using System.Net.Http;
using OpenWeatherApi;
using System.Configuration;
using CurrencyConverter;
using System;

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

        public CurrencyConversionResponse GetCurrencyCoverted(string baseCurrency, string toCurrency)
        {
            CurrencyConverterClient client = new CurrencyConverterClient(new HttpClient());

            var baseCR = MapEnum(baseCurrency);

            var toCR = MapEnum(toCurrency);

            var response = client.LatestAsync(baseCR.ToString()).Result;
            currency = new CurrencyConversionResponse();

            currency.result = toCR switch
            {
                CurrencyEnum.CAD => response.Rates.CAD,
                CurrencyEnum.HKD => response.Rates.HKD,
                CurrencyEnum.ISK => response.Rates.ISK,
                CurrencyEnum.PHP => response.Rates.PHP,
                CurrencyEnum.DKK => response.Rates.DKK,
                CurrencyEnum.HUF => response.Rates.HUF,
                CurrencyEnum.CZK => response.Rates.CZK,
                CurrencyEnum.GBP => response.Rates.GBP,
                CurrencyEnum.RON => response.Rates.RON,
                CurrencyEnum.SEK => response.Rates.SEK,
                CurrencyEnum.IDR => response.Rates.IDR,
                CurrencyEnum.INR => response.Rates.INR,
                CurrencyEnum.BRL => response.Rates.BRL,
                CurrencyEnum.RUB => response.Rates.RUB,
                CurrencyEnum.HRK => response.Rates.HRK,
                CurrencyEnum.JPY => response.Rates.JPY,
                CurrencyEnum.THB => response.Rates.THB,
                CurrencyEnum.CHF => response.Rates.CHF,
                CurrencyEnum.EUR => response.Rates.EUR,
                CurrencyEnum.MYR => response.Rates.MYR,
                CurrencyEnum.BGN => response.Rates.BGN,
                CurrencyEnum.TRY => response.Rates.TRY,
                CurrencyEnum.CNY => response.Rates.CNY,
                CurrencyEnum.NOK => response.Rates.NOK,
                CurrencyEnum.NZD => response.Rates.NZD,
                CurrencyEnum.ZAR => response.Rates.ZAR,
                CurrencyEnum.USD => response.Rates.USD,
                CurrencyEnum.MXN => response.Rates.MXN,
                CurrencyEnum.SGD => response.Rates.SGD,
                CurrencyEnum.AUD => response.Rates.AUD,
                CurrencyEnum.ILS => response.Rates.ILS,
                CurrencyEnum.KRW => response.Rates.KRW,
                CurrencyEnum.PLN => response.Rates.PLN,
                _ => response.Rates.USD,
            };

            return currency;
        }

        static readonly Func<string, CurrencyEnum> MapEnum = (string currency) => (CurrencyEnum)Enum.Parse(typeof(CurrencyEnum), currency);
    }
}
