using System;
using System.Linq;
using yahooapi;

namespace turtles_min
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: turtles-add symbol1");
                return;
            }

            var symbol = args[0];

            var dataProvider = new YahooFinanceProvider();
            var indicators = new IndicatorsCalculator(dataProvider);

            var min = indicators.MinLast20DaysAsync(symbol).Result;
            Console.WriteLine($"The minimum for the last 4 weeks for {symbol} is {min}");
        }
    }
}
