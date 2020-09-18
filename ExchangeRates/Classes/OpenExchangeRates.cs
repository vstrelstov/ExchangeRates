using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Meziantou.OpenExchangeRates;

namespace ExchangeRates.Classes
{
// TODO: Rename this class into OpenExchangeRatesFacade
    public class OpenExchangeRates
    {
        private readonly OpenExchangeRatesClient _oerClient;

        public OpenExchangeRates()
        {
            string apiKey = ConfigurationManager.AppSettings["OpenExchangeRates.ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("OpenExchangeRates.org API key is missing. Provide valid API key to retrieve ecxhange rates");
            _oerClient = new OpenExchangeRatesClient { AppId = apiKey };
        }

        public async Task<Dictionary<DateTime, decimal>> RetrieveRates(IEnumerable<DateTime> dates, string currency)
        {
            var retrievedRates = new Dictionary<DateTime, decimal>();
            foreach (var date in dates)
            {
                var recievedRate = await _oerClient.GetExchangeRatesAsync(date);
                retrievedRates.Add(date, recievedRate.Rates[currency]);
            }

            return retrievedRates;
        }
    }
}
