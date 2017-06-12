using System;
using System.Linq;
using Xunit;
using yahooapi;
using yahooapi.Domain;

namespace yahooapi.unittests
{
    public class TrCalculatorUnitTests
    {
        // Testcases from here
        // http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:average_true_range_atr

        [Fact]
        public void FirstElementReturnsHighLessLow()
        {
            var candles = new Candle[] 
            {
                new Candle { High = 48.7M, Low = 47.79M, Close = 48.16M }
            };

            var calculator = new TrCalculator();
            var trs = calculator.Calculate(candles).ToArray();

            Assert.Equal(1, trs.Length);
            Assert.Equal(0.91M, trs.Last());
        }

        [Fact]
        public void HighLessLowIsHighest()
        {
            var candles = new Candle[] 
            {
                new Candle { High = 49.5M, Low = 48.64M, Close = 49.03M },
                new Candle { High = 49.20M, Low = 48.94M, Close = 49.07M }
            };

            var calculator = new TrCalculator();
            var trs = calculator.Calculate(candles).ToArray();

            Assert.Equal(2, trs.Length);
            Assert.Equal(0.26M, trs.Last());
        }

        [Fact]
        public void PreviousHighLessCloseIsHighest()
        {
            var candles = new Candle[] 
            {
                new Candle { High = 49.35M, Low = 48.86M, Close = 49.32M },
                new Candle { High = 49.92M, Low = 49.50M, Close = 49.91M }
            };

            var calculator = new TrCalculator();
            var trs = calculator.Calculate(candles).ToArray();

            Assert.Equal(2, trs.Length);
            Assert.Equal(0.60M, trs.Last());
        }

        [Fact]
        public void PreviousLowLessCloseIsHighest()
        {
            var candles = new Candle[] 
            {
                new Candle { High = 50.19M, Low = 49.87M, Close = 50.13M },
                new Candle { High = 50.12M, Low = 49.20M, Close = 49.53M }
            };

            var calculator = new TrCalculator();
            var trs = calculator.Calculate(candles).ToArray();

            Assert.Equal(2, trs.Length);
            Assert.Equal(0.93M, trs.Last());
        }
    }

    public class AtrCalculatorUnitTests
    {
        // Test cases from here
        // http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:average_true_range_atr

        protected decimal[] TestData = new decimal[] {
            0.91M, 0.58M, 0.51M, 0.50M, 0.58M, 0.41M, 0.26M, 0.49M, 0.60M, 0.32M, 0.93M, 0.76M, 0.45M, 0.46M, 1.10M
        };

        [Fact]
        public void FirstAtrIsTheAverageOfFirst14TrValues()
        {
            var trs = TestData.Take(14);

            var calculator = new AtrCalculator();
            var atrs = calculator.Calculate(trs).ToArray();

            Assert.Equal(1, atrs.Length);
            // Cannot find equal assert with threshold
            var abs = Math.Abs(0.56M - atrs.Last());
            Assert.True(abs < 0.01M);
        }

        [Fact]
        public void AtrValueIsRight()
        {
            var calculator = new AtrCalculator();
            var atrs = calculator.Calculate(TestData).ToArray();

            Assert.Equal(2, atrs.Length);
            // Cannot find equal assert with threshold
            var abs = Math.Abs(0.59M - atrs.Last());
            Assert.True(abs < 0.01M);
        }
    }
}
