using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRates.Models;
using ExchangeRates.Persistence;

namespace ExchangeRates.Classes
{
    public class CacheManager
    {
        private readonly ExchangeRateContext _context;

        public CacheManager()
        {
            _context = new ExchangeRateContext();
        }

        public async Task Cache(Dictionary<DateTime, decimal> rates, string currency)
        {
            foreach (var rate in rates)
            {
                _context.ExchangeRates.Add(new ExchangeRate
                {
                    Currency = currency,
                    Date = rate.Key,
                    Rate = rate.Value
                });
            }
            await _context.SaveChangesAsync();
        }

        public Dictionary<DateTime, decimal> RetrieveFromCache(DateTime startDate, DateTime finishDate, string currency)
        {
            return _context.ExchangeRates
                .Where(x => x.Date >= startDate && x.Date <= finishDate && x.Currency == currency).OrderBy(y => y.Date)
                .ToDictionary(x => x.Date, y => y.Rate);
        }
    }
}