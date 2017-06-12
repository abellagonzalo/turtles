using System.Collections.Generic;

namespace yahooapi.Domain
{
    public class Meta
    {
        public string currency { get; set; }
        public string symbol { get; set; }
        public string exchangeName { get; set; }
        public string instrumentType { get; set; }
        public int? firstTradeDate { get; set; }
        public int gmtoffset { get; set; }
        public string timezone { get; set; }
        public double previousClose { get; set; }
        public int scale { get; set; }
        public CurrentTradingPeriod currentTradingPeriod { get; set; }
        public List<List<TradingPeriod>> tradingPeriods { get; set; }
        public string dataGranularity { get; set; }
        public List<string> validRanges { get; set; }
    }
}
