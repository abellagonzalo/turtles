namespace yahooapi
{
    public class TimePeriodToYahooTimeConverter
    {
        public string Convert(TimePeriod period)
        {
            if (period.Years > 0)
                return period.Years + "y";

            if (period.Months > 0)
                return period.Months + "mo";

            if (period.Weeks > 0)
                return period.Weeks + "wk";

            if (period.Days > 0)
                return period.Days + "d";

            return "0d";
        }
    }
}
