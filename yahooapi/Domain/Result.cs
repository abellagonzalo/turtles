using System.Collections.Generic;

namespace yahooapi.Domain
{
    public class Result
    {
        public Meta meta { get; set; }
        public List<int> timestamp { get; set; }
        public Indicators indicators { get; set; }
    }
}
