using System.Collections.Generic;
using System.Threading.Tasks;
using yahooapi.Domain;

namespace yahooapi
{
    public interface IStockDataProvider
    {
        Task<RootObject> FetchDataAsync(string symbol, TimePeriod range, TimePeriod interval);
        
        Task<IEnumerable<Candle>> FetchCandlesAsync(string symbol, TimePeriod range, TimePeriod interval);
    }
}
