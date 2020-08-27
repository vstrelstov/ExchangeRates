using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using ExchangeRates.Classes;
using ExchangeRates.Models;

namespace ExchangeRates.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExchangeRateFacade _exchangeRate;

        public HomeController()
        {
            _exchangeRate = new ExchangeRateFacade();    
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IndexViewModel model)
        {
            if (model.StartDate > model.FinishDate)
                ModelState.AddModelError("StartDate", "Start date should be less than finish date");

            if (model.FinishDate > model.StartDate.AddMonths(2))
                ModelState.AddModelError("StartDate", "Selected period should not exceed two months");

            if (ModelState.IsValid)
            {
                var result = await _exchangeRate.GetExchangeRates(model);

                var dates = new List<string>();
                foreach (var date in result.Keys)
                {
                    dates.Add(date.ToShortDateString());
                }
                var rates = result.Values.Cast<object>().ToArray();

                var chart = new Highcharts("ExchangeRatesChart");
                chart.InitChart(new Chart
                {
                    Type = ChartTypes.Line
                }).SetTitle(new Title
                {
                    Text = "Exchange rates of " + model.Currency
                }).SetXAxis(new XAxis()
                {
                    Type = AxisTypes.Linear,
                    Title = new XAxisTitle { Text = "Date" },
                    Categories = dates.ToArray()
                }).SetYAxis(new YAxis
                {
                    Title = new YAxisTitle { Text = "Rate" }
                }).SetSeries(new Series
                {
                    Name = model.Currency,
                    Data = new Data(rates)
                });

                ViewBag.Chart = chart;
            }

            return View(model);
        }
    }
}