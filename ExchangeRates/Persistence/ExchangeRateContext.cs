using System.Data.Entity;
using ExchangeRates.Models;

namespace ExchangeRates.Persistence
{
    public class ExchangeRateContext: DbContext
    {
        public ExchangeRateContext() : base("DbConnection")
        {
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}