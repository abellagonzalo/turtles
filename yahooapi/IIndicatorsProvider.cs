using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yahooapi.Domain;

namespace yahooapi
{
    public interface IIndicatorsProvider
    {
        Task<decimal> MinLast20DaysAsync(string symbol);

        Task<decimal> MaxLast10YearsAsync(string symbol);

        Task<IEnumerable<decimal>> CalculateTrAsync(string symbol);

        Task<IEnumerable<decimal>> CalculateAtrAsync(string symbol);
    }

    public class IndicatorsCalculator : IIndicatorsProvider
    {
        private readonly IStockDataProvider _dataProvider;

        public IndicatorsCalculator(IStockDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<IEnumerable<decimal>> CalculateAtrAsync(string symbol)
        {
            var candles = await _dataProvider.FetchCandlesAsync(symbol, TimePeriod.InYears(5), TimePeriod.InWeeks(1));            
            var calculator = new AtrCalculator();            
            return calculator.Calculate(candles);
        }

        public async Task<IEnumerable<decimal>> CalculateTrAsync(string symbol)
        {
            var candles = await _dataProvider.FetchCandlesAsync(symbol, TimePeriod.InYears(5), TimePeriod.InWeeks(1));            
            var calculator = new TrCalculator();
            return calculator.Calculate(candles);
        }

        public async Task<decimal> MaxLast10YearsAsync(string symbol)
        {
            var rootObject = await _dataProvider.FetchDataAsync(symbol, TimePeriod.InYears(10), TimePeriod.InMonths(1));
            return rootObject.chart.result[0].indicators.quote[0].high.Where(x => x.HasValue).Select(x => x.Value).Max();        
        }

        public async Task<decimal> MinLast20DaysAsync(string symbol)
        {
            var rootObject = await _dataProvider.FetchDataAsync(symbol, TimePeriod.InYears(1), TimePeriod.InWeeks(1));
            return rootObject.chart.result[0].indicators.quote[0].low.Where(x => x.HasValue).Select(x => x.Value).Reverse().Take(4).Min();
        }
    }

    public class TrCalculator
    {
        public IEnumerable<decimal> Calculate(IEnumerable<Candle> candles)
        {
            var previous = candles.First();
            foreach(var current in candles)
            {
                var a = Math.Abs(previous.Close - current.High);
                var b = Math.Abs(previous.Close - current.Low);
                var c = current.High - current.Low;
                yield return Math.Max(Math.Max(a, b), c);
                previous = current;
            }
        }
    }

    public class AtrCalculator
    {
        public IEnumerable<decimal> Calculate(IEnumerable<Candle> candles)
        {
            var calculator = new TrCalculator();
            var trs = calculator.Calculate(candles);
            return Calculate(trs);
        }

        public IEnumerable<decimal> Calculate(IEnumerable<decimal> trs)
        {
            // First ATR is the average of the first 14 TR values
            var previousAtr =  trs.Take(14).Average();
            yield return previousAtr;

            // Current ATR = [(Prior ATR x 13) + Current TR] / 14
            foreach (var tr in trs.Skip(14))
            {
                previousAtr = (previousAtr * 13 + tr) / 14;
                yield return previousAtr;
            }
        }
    }
}