using BankApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.BLL.Services
{
    /// <summary>
    /// Implementation of currency rates service
    /// </summary>
    public class CurrencyRatesService : ICurrencyRatesService
    {
        public async Task<Dictionary<string, List<decimal>>> GetCurrencyRatesBuy()
        {
            var exchangeRates = new Dictionary<string, List<decimal>>();
            string line = "";

            using (var webClient = new WebClient())
            {
                line = await webClient.DownloadStringTaskAsync("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");
            }

            NumberFormatInfo formatInfo = new NumberFormatInfo();
            formatInfo.NumberDecimalSeparator = ".";

            Match matchUSD = Regex.Match(line, "{\"ccy\":\"USD\",\"base_ccy\":\"UAH\",\"buy\":\"(.*?)\",\"sale\":\"(.*?)\"}");
            SetExchangeRates(exchangeRates, formatInfo, matchUSD, "USD");

            Match matchRUB = Regex.Match(line, "{\"ccy\":\"RUR\",\"base_ccy\":\"UAH\",\"buy\":\"(.*?)\",\"sale\":\"(.*?)\"}");
            SetExchangeRates(exchangeRates, formatInfo, matchRUB, "RUB");

            Match matchEUR = Regex.Match(line, "{\"ccy\":\"EUR\",\"base_ccy\":\"UAH\",\"buy\":\"(.*?)\",\"sale\":\"(.*?)\"}");
            SetExchangeRates(exchangeRates, formatInfo, matchEUR, "EUR");

            Match matcBTC = Regex.Match(line, "{\"ccy\":\"BTC\",\"base_ccy\":\"USD\",\"buy\":\"(.*?)\",\"sale\":\"(.*?)\"}");
            SetExchangeRates(exchangeRates, formatInfo, matcBTC, "BTC");

            return exchangeRates;
        }

        private static void SetExchangeRates(Dictionary<string, List<decimal>> exchangeRates, NumberFormatInfo formatInfo, Match matchUSD, string currency)
        {
            var exchangeRateBuyUSD = Math.Round(Convert.ToDecimal(matchUSD.Groups[1].Value, formatInfo), 2);
            var exchangeRateSaleUSD = Math.Round(Convert.ToDecimal(matchUSD.Groups[2].Value, formatInfo), 2);
            exchangeRates.Add(currency, new List<decimal> { exchangeRateBuyUSD, exchangeRateSaleUSD });
        }
    }
}
