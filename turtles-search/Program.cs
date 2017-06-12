using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace turtles_search
{
    public class SearchData
    {
        public string SuggestionTitleAccessor { get; set; }
        public string[] SuggestionMeta { get; set; } = new string[0];
        public SearchItem[] Items { get; set; } = new SearchItem[0];
    }

    public class SearchItem
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exch { get; set; }
        public string Type { get; set; }
        public string exchDisp { get; set; }
        public string typeDisp { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: turtles-search symbol1");
                return;
            }

            var symbol = args[0];
            Console.WriteLine($"Searching symbols matching '{symbol}'...");

            var client = new RestClient("https://finance.yahoo.com");
            var request = new RestRequest($"_finance_doubledown/api/resource/searchassist;searchTerm={symbol}");

            var taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync(
                request, r => taskCompletion.SetResult(r));

            var response = taskCompletion.Task.Result;
            var theSearch = JsonConvert.DeserializeObject<SearchData>(response.Content);

            foreach (var item in theSearch.Items)
                Console.WriteLine($"{item.Symbol} - {item.Name}");
            Console.WriteLine("");
        }
    }
}
