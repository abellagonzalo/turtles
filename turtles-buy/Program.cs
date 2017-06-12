using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using yahooapi;

namespace turtles_buy
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: turtles-buy symbols.txt");
                return;
            }

            var headers = new string[] {
                "Valor", "Riesgo", "Precio de compra", "ATR", "Precio de salida", "# de acciones", "Cash necesario"
            };
            Console.WriteLine(string.Join("\t", headers));

            var symbols = File.ReadAllLines(args[0]).Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Trim());

            var i = 2;
            foreach(var symbol in symbols)
            {
                var dataProvider = new YahooFinanceProvider();
                var indicators = new IndicatorsCalculator(dataProvider);

                var max = indicators.MaxLast10YearsAsync(symbol);
                Thread.Sleep(1000);
                var atr = indicators.CalculateAtrAsync(symbol);
                Thread.Sleep(1000);
                var tr = indicators.CalculateTrAsync(symbol);
                Thread.Sleep(1000);

                var maxRounded = Math.Round(max.Result, 2);
                var atrRounded = Math.Round(atr.Result.Last(), 2);

                var maxWithCommas = maxRounded.ToString().Replace('.', ',');
                var atrWithCommas = atrRounded.ToString().Replace('.', ',');

                Console.Write(symbol); Console.Write("\t");
                Console.Write(200); Console.Write("\t");
                Console.Write(maxWithCommas); Console.Write("\t");
                Console.Write(atrWithCommas); Console.Write("\t");
                Console.Write($"=C{i}-D{i}"); Console.Write("\t");
                Console.Write($"=FLOOR(B{i}/D{i})"); Console.Write("\t");
                Console.Write($"=C{i}*F{i}"); Console.WriteLine("");

                i++;
            }
        }

    }
}
