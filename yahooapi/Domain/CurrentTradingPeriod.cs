namespace yahooapi.Domain
{
    public class CurrentTradingPeriod
    {
        public TradingPeriod pre { get; set; }
        public TradingPeriod regular { get; set; }
        public TradingPeriod post { get; set; }
    }
}
