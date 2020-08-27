using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeRates.Attributes
{
    public class DateTimeRangeAttribute: ValidationAttribute
    {
        // 01.01.1999 - the earliest date for which exchange rates are available
        private DateTime OpenExchangeRateMinDate = new DateTime(1999, 1, 1);

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            var date = (DateTime) value;
            return date >= OpenExchangeRateMinDate && date <= DateTime.Now;
        }
    }
}