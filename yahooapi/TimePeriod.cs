namespace yahooapi
{
    public class TimePeriod
    {
        public static TimePeriod InYears(int n)
        {
            return new TimePeriod { Years = n };
        }

        public static TimePeriod InMonths(int n)
        {
            return new TimePeriod { Months = n };
        }

        public static TimePeriod InWeeks(int n)
        {
            return new TimePeriod { Weeks = n };
        }

        public static TimePeriod InDays(int n)
        {
            return new TimePeriod { Days = n };
        }

        public int Years { get; private set; }
        public int Months { get; private set; }
        public int Weeks { get; private set; }
        public int Days { get; private set; }
    }
}
