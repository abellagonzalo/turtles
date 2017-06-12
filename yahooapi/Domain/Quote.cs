using System.Collections.Generic;

namespace yahooapi.Domain
{
    public class Quote
    {
        public List<decimal?> open { get; set; }
        public List<decimal?> close { get; set; }
        public List<decimal?> high { get; set; }
        public List<decimal?> low { get; set; }
        public List<int?> volume { get; set; }
    }
}
