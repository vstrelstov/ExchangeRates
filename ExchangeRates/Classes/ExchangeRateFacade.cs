using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRates.Models;

namespace ExchangeRates.Classes
{
    public class ExchangeRateFacade
    {
        private readonly OpenExchangeRates _openExchangeRates;
        private readonly CacheManager _cacheManager;

        public ExchangeRateFacade()
        {
            _openExchangeRates = new OpenExchangeRates();
            _cacheManager = new CacheManager();
        }

        public async Task<Dictionary<DateTime, decimal>> GetExchangeRates(IndexViewModel model)
        {
            var allDates = Enumerable.Range(0, 1 + model.FinishDate.Subtract(model.StartDate).Days)
                .Select(x => model.StartDate.AddDays(x)).ToList();
            var ratesFromCache = _cacheManager.RetrieveFromCache(model.StartDate, model.FinishDate, model.Currency);
            var ratesForCaching = await _openExchangeRates.RetrieveRates(allDates.Except(ratesFromCache.Keys), model.Currency);
            await _cacheManager.Cache(ratesForCaching, model.Currency);
            ratesForCaching.ToList().ForEach(x => ratesFromCache.Add(x.Key, x.Value));
            return ratesFromCache;
        }
    }
}