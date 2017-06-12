using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using yahooapi.Domain;

namespace yahooapi
{
    public class YahooFinanceProvider : IStockDataProvider
    {
        // https://query2.finance.yahoo.com/v7/finance/chart/hot.de?range=1mo&interval=1d

        private string _yahooUrl = "https://query2.finance.yahoo.com";

        public async Task<RootObject> FetchDataAsync(string symbol, TimePeriod range, TimePeriod interval)
        {
            var converter = new TimePeriodToYahooTimeConverter();
            var rangeStr = converter.Convert(range);
            var intervalStr = converter.Convert(interval);            

            var client = new RestClient(_yahooUrl);
            var request = new RestRequest($"v7/finance/chart/{symbol}?range={rangeStr}&interval={intervalStr}");
            Console.Error.WriteLine(client.BuildUri(request));

            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));

            var response = await taskCompletion.Task;

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            var rootObject = JsonConvert.DeserializeObject<RootObject>(response.Content);

            if (!rootObject.chart.result.Any())
                Console.WriteLine($"Symbol {symbol} does not exist");

            return rootObject;
        }

        public async Task<IEnumerable<Candle>> FetchCandlesAsync(string symbol, TimePeriod range, TimePeriod interval)
        {
            var rootObject = await FetchDataAsync(symbol, range, interval);
            return CreateCandles(rootObject);
        }

        private IEnumerable<Candle> CreateCandles(RootObject rootObject)
        {
            var result = rootObject.chart.result[0];
            var quote = result.indicators.quote[0];
            var n = result.timestamp.Count;

            for (var i=0; i<n; i++)
            {
                if (!quote.volume[i].HasValue) continue;

                yield return new Candle
                {
                    High = quote.high[i].Value,
                    Low = quote.low[i].Value,
                    Open = quote.open[i].Value,
                    Close = quote.close[i].Value,
                    Volume = quote.volume[i].Value,
                    DateTime = ToDateTime(result.timestamp[i])
                };
            }
        }

        private DateTime _zeroTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);

        private DateTime ToDateTime(int timestamp)
        {
            return _zeroTime + TimeSpan.FromSeconds(timestamp);
        }
    }
}
