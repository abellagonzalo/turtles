using System.Collections.Generic;

namespace yahooapi.Domain
{
    public class Chart
    {
        public List<Result> result { get; set; }
        public object error { get; set; }
    }
}
